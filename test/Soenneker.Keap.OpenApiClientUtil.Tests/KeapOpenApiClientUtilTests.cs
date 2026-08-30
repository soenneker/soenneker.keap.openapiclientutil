using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Keap.HttpClients.Abstract;
using Soenneker.Keap.OpenApiClientUtil.Abstract;
using Soenneker.Keap.OpenApiClientUtil.Registrars;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Keap.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class KeapOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IKeapOpenApiClientUtil _openapiclientutil;

    public KeapOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IKeapOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }

    [Test]
    public async Task Scoped_utility_keeps_http_client_singleton()
    {
        var services = new ServiceCollection();

        services.AddKeapOpenApiClientUtilAsScoped();

        ServiceDescriptor httpClient = services.Single(descriptor => descriptor.ServiceType == typeof(IKeapOpenApiHttpClient));
        ServiceDescriptor clientUtil = services.Single(descriptor => descriptor.ServiceType == typeof(IKeapOpenApiClientUtil));

        await Assert.That(httpClient.Lifetime).IsEqualTo(ServiceLifetime.Singleton);
        await Assert.That(clientUtil.Lifetime).IsEqualTo(ServiceLifetime.Scoped);
    }
}
