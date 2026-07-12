using Kondongpu.Infrastructure.Persistence;
using Kondongpu.Infrastructure.Persistence.Interceptors;
using Kondongpu.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Kondongpu.Application.Common.Interfaces;

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
        services.AddScoped(provider => (Application.Common.Interfaces.IKondongpuDatabaseContext)provider.GetService<Persistence.KondongpuDatabaseContext>());
        services.AddScoped<KondongpuDatabaseContextInitializer>();
        services.AddTransient<IDateTime, DateTimeService>();
        return services;
    }
}
