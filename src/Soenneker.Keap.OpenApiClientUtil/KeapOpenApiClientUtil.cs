using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Keap.HttpClients.Abstract;
using Soenneker.Keap.OpenApiClientUtil.Abstract;
using Soenneker.Keap.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Keap.OpenApiClientUtil;

/// <inheritdoc cref="IKeapOpenApiClientUtil"/>
public sealed class KeapOpenApiClientUtil : IKeapOpenApiClientUtil
{
    private readonly AsyncSingleton<KeapOpenApiClient> _client;

    public KeapOpenApiClientUtil(IKeapOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<KeapOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Keap:AccessToken");
            string authHeaderName = configuration["Keap:AuthHeaderName"] ?? "Authorization";
            string authHeaderValueTemplate = configuration["Keap:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerName: authHeaderName, headerValue: authHeaderValue),
                httpClient: httpClient);

            return new KeapOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<KeapOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
