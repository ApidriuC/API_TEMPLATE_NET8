using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskProject.Manager.Application.Validators;
using TaskProject.Manager.Base.Behaviors;
using TaskProject.Manager.Base.Extensions;
using TaskProject.Manager.Domain.Interfaces;
using TaskProject.Manager.Infrastructure.Repositories;

namespace TaskProject.Manager.Application.Configuration;

/// <summary>
/// Configurar y extender el contenedor de dependencias.
/// </summary>
public static class ConfigContainer
{
    /// <summary>
    /// Agregar dependencias del dominio.
    /// </summary>
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddBaseContainer();
        services.AddMediatorContainer();
        services.AddValidatorContainer();
        return services;
    }

    internal static IServiceCollection AddBaseContainer(this IServiceCollection services)
    {
        // Repositorios.
        services.AddTransient<IDataManager, DataManagerRepository>();
        return services;
    }

    /// <summary>
    /// Inyectar dependencias del mediador.
    /// </summary>
    internal static IServiceCollection AddMediatorContainer(this IServiceCollection services)
    {
        services.AddMediatR(config => config
            .RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );

        // Registrar comportamientos (Agregar en el orden que se disparen en la canalizacion).
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidateBehavior<,>));
        return services;
    }

    /// <summary>
    /// Inyectar dependencias de los validadores.
    /// </summary>
    internal static IServiceCollection AddValidatorContainer(this IServiceCollection services)
    {
        return services.AddAsImplementedInterfaces(typeof(IMatchValidator), filter: "Validator");
    }
}
