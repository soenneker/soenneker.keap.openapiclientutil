[![](https://img.shields.io/nuget/v/soenneker.keap.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.keap.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.keap.openapiclientutil/build-and-test.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.keap.openapiclientutil/actions/workflows/build-and-test.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.keap.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.keap.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.keap.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.keap.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.keap.openapiclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.keap.openapiclientutil/actions/workflows/codeql.yml)

# Soenneker.Keap.OpenApiClientUtil

Provides a lazily created Keap Kiota client over the shared Keap `HttpClient`.

## Install

```bash
dotnet add package Soenneker.Keap.OpenApiClientUtil
```

## Configuration

```json
{
  "Keap": {
    "AccessToken": "<OAuth access token>"
  }
}
```

`AccessToken` is required. The default base address is `https://api.infusionsoft.com/crm`, and authentication defaults to `Authorization: Bearer {token}`. Override `Keap:ClientBaseUrl`, `Keap:AuthHeaderName`, or `Keap:AuthHeaderValueTemplate` when using another compatible endpoint.

## Register

```csharp
using Soenneker.Keap.OpenApiClientUtil.Registrars;

services.AddKeapOpenApiClientUtilAsScoped();
```

The scoped registration deliberately keeps `IKeapOpenApiHttpClient` singleton. Disposing a scope releases that utility's generated-client wrapper without tearing down the long-lived HTTP client used by later scopes.

Use `AddKeapOpenApiClientUtilAsSingleton()` when the generated-client wrapper should also live for the application lifetime.

## Usage

```csharp
using Soenneker.Keap.OpenApiClient;
using Soenneker.Keap.OpenApiClient.Models;
using Soenneker.Keap.OpenApiClientUtil.Abstract;

public sealed class ContactReader(IKeapOpenApiClientUtil clientUtil)
{
    public async Task<ListContactsResponse?> List(
        string? pageToken,
        CancellationToken cancellationToken)
    {
        KeapOpenApiClient client = await clientUtil.Get(cancellationToken);

        return await client.Rest.V2.Contacts.GetAsync(config =>
        {
            config.QueryParameters.PageSize = 100;
            config.QueryParameters.PageToken = pageToken;
        }, cancellationToken);
    }
}
```

Repeated and concurrent `Get()` calls on the same utility instance reuse its lazily initialized generated client. Cancellation affects first-time initialization; pass the token separately to generated request methods.

Let the dependency-injection container dispose the utility. Do not dispose the shared `HttpClient` obtained by the lower-level package.
