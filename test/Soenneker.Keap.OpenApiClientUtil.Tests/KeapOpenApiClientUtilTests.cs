using Soenneker.Keap.OpenApiClientUtil.Abstract;
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
}
