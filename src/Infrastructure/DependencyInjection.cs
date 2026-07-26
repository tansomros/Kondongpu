using Kondongpu.Application.Common.Interfaces;
using Kondongpu.Application.Identity.Interfaces;
using Kondongpu.Infrastructure.Identity;
using Kondongpu.Infrastructure.Persistence;
using Kondongpu.Infrastructure.Persistence.Interceptors;
using Kondongpu.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using IIdentityService = Kondongpu.Application.Identity.Interfaces.IIdentityService; 

namespace Kondongpu.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddKondongpuInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptors>();

        // enable json store
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("KondongpuDatabase"));
        dataSourceBuilder.EnableDynamicJson();
        var dataSource = dataSourceBuilder.Build();
        services.AddDbContext<Persistence.KondongpuDatabaseContext>(options => options.UseNpgsql(dataSource));
        //services.AddScoped(provider => (Application.Common.Interfaces.IKondongpuDatabaseContext)provider.GetRequiredService<Persistence.KondongpuDatabaseContext>());
        services.AddScoped<IKondongpuDatabaseContext>(provider =>
    provider.GetRequiredService<KondongpuDatabaseContext>());
        services.AddScoped<KondongpuDatabaseContextInitializer>();

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();    
        services.AddScoped<IJwtTokenService, JwtTokenService>();    
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddTransient<IDateTime, DateTimeService>();
        return services;
    }
}
