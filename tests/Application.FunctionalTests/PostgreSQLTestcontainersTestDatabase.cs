using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Respawn;
using Kondongpu.Infrastructure.Persistence;
using Testcontainers.PostgreSql;

namespace Kondongpu.Application.FunctionalTests;

public class PostgreSQLTestContainersTestDatabase : ITestDatabase
{
    private const string DefaultDatabase = "SUTH_CheckupTestDb";
    private readonly PostgreSqlContainer _container;
    private DbConnection _connection = null!;
    private string _connectionString = null!;
    private Respawner _respawner = null!;

    public PostgreSQLTestContainersTestDatabase()
    {
        _container = new PostgreSqlBuilder("postgres:14")
            .WithAutoRemove(true)
            .Build();
    }

    public async Task InitialiseAsync()
    {
        await _container.StartAsync();
        await _container.ExecScriptAsync($"CREATE DATABASE {DefaultDatabase}");

        var builder = new NpgsqlConnectionStringBuilder(_container.GetConnectionString())
        {
            Database = DefaultDatabase
        };

        _connectionString = builder.ConnectionString;

        _connection = new NpgsqlConnection(_connectionString);

        var options = new DbContextOptionsBuilder<CheckupDatabaseContext>()
            .UseNpgsql(_connectionString)
            .Options;

        var context = new CheckupDatabaseContext(options);

        await context.Database.MigrateAsync();

        await _connection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            TablesToIgnore = ["__EFMigrationsHistory"]
        });
        await _connection.CloseAsync();
    }

    public DbConnection GetConnection()
    {
        return _connection;
    }

    public string GetConnectionString()
    {
        return _connectionString;
    }

    public async Task ResetAsync()
    {
        await _connection.OpenAsync();
        await _respawner.ResetAsync(_connection);
        await _connection.CloseAsync();
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
        await _container.DisposeAsync();
    }
}
