using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using PruebaTecnicaImaginemos.Application.Abstraction.Behaviors;
using System.Reflection;

namespace PruebaTecnicaImaginemos.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));

            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));

        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
