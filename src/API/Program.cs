using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kondongpu.Application;
using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Common.Security;
using Kondongpu.Infrastructure;
using Kondongpu.Infrastructure.Identity;
using Kondongpu.Infrastructure.Persistence;
using Kondongpu.Presentation.API.Converters;
using Kondongpu.Presentation.API.Middlewares;
using Kondongpu.Presentation.API.Routing;
using Kondongpu.Presentation.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Kondongpu.Presentation.API;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttpClient();
        builder.Services.AddKondongpuInfrastructure(builder.Configuration);
        builder.Services.AddKondongpuApplication();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddCors();

        builder.Services.AddDataProtection()
            .SetApplicationName("Kondongpu")
            .PersistKeysToFileSystem(new DirectoryInfo(@"/app/publish/secret"));

        builder.Services
            .AddControllers(options => options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer())))
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
                options.JsonSerializerOptions.Converters.Add(new JsonDateOnlyConverter());
                options.JsonSerializerOptions.Converters.Add(new JsonTimeOnlyConverter());
            });

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
            options.SuppressMapClientErrors = true;
        });

        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
        var useDevelopmentBypass = builder.Configuration.GetValue<bool>("Identity:UseDevelopmentBypass");
        var authority = builder.Configuration["Identity:Authority"];

        if (useDevelopmentBypass)
        {
            // Dev bypass: ไม่ต้องตรวจสอบ JWT — DevAuthenticationMiddleware จะจัดการเอง
            builder.Services.AddAuthentication();
        }
        else if (!string.IsNullOrEmpty(authority))
        {
            builder.Services
                .AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = authority;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });
        }
        else
        {
            Console.WriteLine("Warning: Please config identity authority in appsetting.Environment.json. Skipping Identity configuration.");
        }
        //----------------Identity JWt-------------------
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwt = builder.Configuration
            .GetSection(JwtSettings.SectionName)
            .Get<JwtSettings>()!;

        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwt.Issuer,
                ValidAudience = jwt.Audience,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwt.SecretKey))
            };
    });

        builder.Services.AddAuthorization();

        //---------------------------------------

        builder.Services.AddMemoryCache();
        //builder.Services.AddScoped<ICheckupItemCacheService, CheckupItemCacheService>();
        //builder.Services.AddScoped<ICheckupStatusService, CheckupStatusService>();

        // basic policy
        // this authorization should be config in the infrastructure?, revise later
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(KondongpuPolicies.RequireAuthenticatedUser, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "kondongpu-api");
                policy.RequireClaim("scope", "openid");
                policy.RequireClaim("scope", "profile");
                policy.RequireClaim("scope", "offline_access");
            });

            options.AddPolicy(KondongpuPolicies.RequireDoctor, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole(KondongpuRoles.Doctor);
                policy.RequireClaim("scope", "kondongpu-api");
                policy.RequireClaim("scope", "openid");
                policy.RequireClaim("scope", "profile");
                policy.RequireClaim("scope", "offline_access");
                policy.RequireClaim("scope", "license");
            });

            options.AddPolicy(KondongpuPolicies.RequireNurse, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole(KondongpuRoles.Nurse);
                policy.RequireClaim("scope", "kondongpu-api");
                policy.RequireClaim("scope", "openid");
                policy.RequireClaim("scope", "profile");
                policy.RequireClaim("scope", "offline_access");
            });

            options.AddPolicy(KondongpuPolicies.RequireAdmin, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole(KondongpuRoles.Admin);
                policy.RequireClaim("scope", "kondongpu-api");
                policy.RequireClaim("scope", "openid");
                policy.RequireClaim("scope", "profile");
                policy.RequireClaim("scope", "offline_access");
            });

            options.AddPolicy(KondongpuPolicies.RequireEmployee, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole(KondongpuRoles.Employee);
                policy.RequireClaim("scope", "kondongpu-api");
                policy.RequireClaim("scope", "openid");
                policy.RequireClaim("scope", "profile");
                policy.RequireClaim("scope", "offline_access");
                policy.RequireClaim("scope", "employee_id");
            });
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SupportNonNullableReferenceTypes();
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Kondongpu API",
                Version = "v1",
                Description = "เอกสารเรียกใช้งาน API Kondongpu",
                Contact = new OpenApiContact
                {
                    Name = "ติดต่อ Kondongpu",
                    Email = "teerapoldev@gmail.com"
                },
                License = new OpenApiLicense
                {
                    Name = "ลิขสิทธิ์ของ Kondongpu",
                    Url = new Uri("https://www.kondongpu.com")
                }
            });

            if (!useDevelopmentBypass && !string.IsNullOrEmpty(authority))
            {
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    OpenIdConnectUrl = new Uri($"{authority}/.well-known/openid-configuration"),
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{authority}/connect/authorize"),
                            TokenUrl = new Uri($"{authority}/connect/token"),
                            Scopes = new Dictionary<string, string>
                                {
                                    { "kondongpu-api", "Kondongpu API" },
                                }
                        }
                    },
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "oauth2",
                                },
                                Scheme = "oauth2",
                                Name = "oauth2",
                                In = ParameterLocation.Header
                            },
                            new List<string>() { "kondongpu-api" }
                        }
                    });
            }
 
            List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "Kondongpu*.xml", SearchOption.TopDirectoryOnly).ToList();
            xmlFiles.ForEach(file => c.IncludeXmlComments(file, includeControllerXmlComments: true));
        });

        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var initializer = scope.ServiceProvider.GetRequiredService<KondongpuDatabaseContextInitializer>();
            await initializer.MigrationAsync();
            await initializer.SeedDataAsync(scope);
        }

        var basePath = new PathString(Environment.GetEnvironmentVariable("ASPNETCORE_BASE_PATH"));
        if (!string.IsNullOrEmpty(basePath))
        {
            app.UsePathBase(basePath);
        }

        app.UseStaticFiles();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.DocumentTitle = "Kondongpu API";
            if (!string.IsNullOrEmpty(basePath))
            {
                c.InjectStylesheet($"{basePath}/swagger/ui/fonts/fonts.css");
                c.InjectStylesheet($"{basePath}/swagger/ui/custom.css");
                c.InjectJavascript($"{basePath}/swagger/ui/custom.js");
                c.SwaggerEndpoint($"{basePath}/swagger/v1/swagger.json", "Kondongpu API v1");
            }
            else
            {
                c.InjectStylesheet("/swagger/ui/fonts/fonts.css");
                c.InjectStylesheet("/swagger/ui/custom.css");
                c.InjectJavascript("/swagger/ui/custom.js");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kondongpu API v1");
            }

            c.DefaultModelsExpandDepth(-1);
            c.ConfigObject.AdditionalItems.Add("syntaxHighlight", true);
            if (!useDevelopmentBypass)
            {
                c.OAuthClientId(builder.Configuration["Identity:ClientId"]);
                c.OAuthAppName("ระบบรายงานผลตรวจสุขภาพ โรงพยาบาลมหาวิทยาลัยเทคโนโลยีสุรนารี");
                c.OAuthUsePkce();
                c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
            }
        });

        if (builder.Environment.IsProduction())
        {
            var forwardedHeaderOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };

            forwardedHeaderOptions.KnownNetworks.Clear();
            forwardedHeaderOptions.KnownProxies.Clear();
            app.UseForwardedHeaders(forwardedHeaderOptions);
        }

        app.UseHttpsRedirection();
        app.UseExceptionMiddleware();
        app.UseRouting();
        app.UseCors(builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });

        if (useDevelopmentBypass)
        {
            app.UseDevAuthentication();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
                name: "default",
                pattern: "{controller:slugify}/{action:slugify}/{id:slugify?}");

        app.Run();
    }
}
