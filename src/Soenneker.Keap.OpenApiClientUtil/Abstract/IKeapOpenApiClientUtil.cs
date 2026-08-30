using Soenneker.Keap.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Soenneker.Keap.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides a lazily initialized Keap API client cached for the utility's lifetime.
/// </summary>
public interface IKeapOpenApiClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the generated client configured to call the Keap API.
    /// </summary>
    /// <param name="cancellationToken">Stops client initialization if the cached instance has not been created yet.</param>
    /// <returns>The generated client cached for this utility's lifetime.</returns>
    ValueTask<KeapOpenApiClient> Get(CancellationToken cancellationToken = default);
}
