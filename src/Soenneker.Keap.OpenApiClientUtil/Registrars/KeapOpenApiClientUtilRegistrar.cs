using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Keap.HttpClients.Registrars;
using Soenneker.Keap.OpenApiClientUtil.Abstract;

namespace Soenneker.Keap.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class KeapOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="KeapOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddKeapOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddKeapOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IKeapOpenApiClientUtil, KeapOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="KeapOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddKeapOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddKeapOpenApiHttpClientAsSingleton()
                .TryAddScoped<IKeapOpenApiClientUtil, KeapOpenApiClientUtil>();

        return services;
    }
}
