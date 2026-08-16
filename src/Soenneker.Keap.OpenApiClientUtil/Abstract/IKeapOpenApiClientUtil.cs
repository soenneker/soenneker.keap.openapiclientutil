using Soenneker.Keap.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Keap.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IKeapOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<KeapOpenApiClient> Get(CancellationToken cancellationToken = default);
}
