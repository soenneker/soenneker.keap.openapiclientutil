[![](https://img.shields.io/nuget/v/soenneker.keap.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.keap.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.keap.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.keap.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.keap.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.keap.openapiclientutil/)

# Soenneker.Keap.OpenApiClientUtil

Exposes a cached OpenAPI client instance.

## Install

```bash
dotnet add package Soenneker.Keap.OpenApiClientUtil
```

## Quick start

```csharp
using Soenneker.Keap.OpenApiClientUtil.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddKeapOpenApiClientUtilAsSingleton();
```

Adds `KeapOpenApiClientUtil` as a singleton service.

## What you get

- `IKeapOpenApiClientUtil` — Exposes a cached OpenAPI client instance.
- `KeapOpenApiClientUtilRegistrar` — Registers the OpenAPI client utility for dependency injection.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `KeapOpenApiClientUtilRegistrar.AddKeapOpenApiClientUtilAsSingleton(services)` | Adds `KeapOpenApiClientUtil` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `KeapOpenApiClientUtilRegistrar.AddKeapOpenApiClientUtilAsScoped(services)` | Adds `KeapOpenApiClientUtil` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Dispose instances you own when their scope ends so held resources can be released.
