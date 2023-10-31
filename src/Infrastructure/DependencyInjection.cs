using Application.Contracts;
using Infrastructure.Database;
using Infrastructure.Database.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, DatabaseConfiguration dbConfig)
    {
        var connectionString = $"server={dbConfig.Host};user={dbConfig.User};password={dbConfig.Password};database={dbConfig.Database}";
        var serverVersion = new MySqlServerVersion(new Version(dbConfig.ServerVersionMajor, dbConfig.ServerVersionMinor));

        services.AddDbContext<Context>(
            dbContextOptions => dbContextOptions
                .UseMySql(connectionString, serverVersion)
                // The following three options help with debugging, but should
                // be changed or removed for production.
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
        );
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}