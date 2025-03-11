using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TaskProject.Manager.Base.Extensions;

/// <summary>
/// Conjunto de herramientas de uso común para la inyección de dependencias.
/// </summary>
public static class InjectionExtensions
{
    /// <summary>
    /// Inyectar dependencias de un namespace. 
    /// Registra el servicio como sus interfaces implementadas
    /// </summary>
    public static IServiceCollection AddAsImplementedInterfaces(
        this IServiceCollection services, Type type, string filter, ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        return services.AddServiceByTargetType(type, filter, false, lifetime);
    }

    /// <summary>
    /// Inyectar dependencias de un namespace
    /// </summary>
    public static IServiceCollection AddAsSelf(
        this IServiceCollection services, Type type, string filter, ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        return services.AddServiceByTargetType(type, filter, true, lifetime);
    }

    private static IServiceCollection AddServiceByTargetType(
        this IServiceCollection services,
        Type targetType,
        string classNameFilter,
        bool implementedAsSelf,
        ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        var targetNamespace = targetType.Namespace ?? string.Empty;
        var targetAssembly = targetType.Assembly;

        var filterTypes = targetAssembly.GetTypes()
            .Where(t =>
                (t.Namespace?.StartsWith(targetNamespace) ?? false) &&
                (t.Name?.EndsWith(classNameFilter) ?? false) && t.IsClass);

        foreach (Type tImplemen in filterTypes)
        {
            if (implementedAsSelf)
            {
                var servDescriptor = new ServiceDescriptor(tImplemen, tImplemen, lifetime);
                services.TryAdd(servDescriptor);
            }
            else
            {
                foreach (Type tInterface in tImplemen.GetInterfaces())
                {
                    var servDescriptor = new ServiceDescriptor(tInterface, tImplemen, lifetime);
                    services.TryAdd(servDescriptor);
                }
            }
        }

        return services;
    }
}
