using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Infraestructure.Extensions;

public static class DbInitializer
{
    public static void ApplyMigrations(IHost host, int maxRetries = 10)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("DbInitializer");

        string connectionString = dbContext.Database.GetConnectionString();

        for (int i = 0; i < maxRetries; i++)
        {
            try
            {
                using var conn = new NpgsqlConnection(connectionString);
                conn.Open();
                logger.LogInformation("✅ Conexión exitosa a PostgreSQL");
                break;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"⏳ Intentando conectar a PostgreSQL ({i + 1}/{maxRetries})...");
                logger.LogWarning(ex.Message);
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        }

        try
        {
            dbContext.Database.Migrate();
            logger.LogInformation("📜 Migraciones aplicadas correctamente");
        }
        catch (Exception ex)
        {
            logger.LogError("❌ Error al aplicar migraciones", ex);
        }
    }
}
