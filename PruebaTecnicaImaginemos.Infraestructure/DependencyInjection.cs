using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebaTecnicaImaginemos.Application.Abstraction.Data;
using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.Interfaces;
using PruebaTecnicaImaginemos.Infraestructure.Data;
using PruebaTecnicaImaginemos.Infraestructure.Repository.EntityRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(
            this IServiceCollection services,
            IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CadenaPostgre")
            ?? throw new ArgumentNullException("Cadena de conexión no encontrada en appsettings.json");

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(connectionString);

            var publisher = serviceProvider.GetRequiredService<IPublisher>();
            options.UseApplicationServiceProvider(serviceProvider);
        });

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationDbContext).Assembly));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<ISaleDetailRepository, SaleDetailRepository>();

        services.AddScoped<IUnitOfWork>(e => e.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ISqlConnectionFactory>(sp =>
        {
            var connectionString = configuration.GetConnectionString("CadenaPostgre");
            return new SqlConnectionFactory(connectionString);
        });

        return services;
    }
}

