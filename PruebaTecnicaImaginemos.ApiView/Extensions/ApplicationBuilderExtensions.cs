using Microsoft.EntityFrameworkCore;
using PruebaTecnicaImaginemos.ApiView.Middleware;
using PruebaTecnicaImaginemos.Infraestructure;

namespace PruebaTecnicaImaginemos.ApiView.Extencions;

public static class ApplicationBuilderExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
