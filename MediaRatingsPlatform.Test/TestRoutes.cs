using MediaRatingsPlatform.PresentationLayer.Endpoints;

namespace MediaRatingsPlatform.Test;
using MediaRatingsPlatform.PresentationLayer;

internal class TestRoutes
{
    [Test]
    public void TestValidRoute()
    {
        List<string?> route = ["api", "leaderboard"];
        HttpMethod method = HttpMethod.Get;
        IHttpEndpoint result = Routes.Route(new HttpRequest { Method = method, Path = route });
        Assert.That(result, Is.TypeOf<LeaderboardEndpoint>());
    }

    [Test]
    public void TestInvalidRoute()
    {
        List<string?> route = ["api", "invalidroute", null];
        HttpMethod method = HttpMethod.Get;
        IHttpEndpoint result = Routes.Route(new HttpRequest { Method = method, Path = route });
        Assert.That(result, Is.TypeOf<BadRequest>());
    }
    
    [Test]
    public void TestInvalidMethodOnRoute()
    {
        List<string?> route = ["api", "leaderboard"];
        HttpMethod method = HttpMethod.Delete;
        IHttpEndpoint result = Routes.Route(new HttpRequest { Method = method, Path = route });
        Assert.That(result, Is.TypeOf<BadRequest>());
    }
}
