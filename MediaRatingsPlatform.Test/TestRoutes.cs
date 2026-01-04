using MediaRatingsPlatform.PresentationLayer;
using MediaRatingsPlatform.PresentationLayer.Endpoints;
using HttpMethod = MediaRatingsPlatform.PresentationLayer.HttpMethod;

namespace MediaRatingsPlatform.Test;

internal class TestRoutes {
    [Test]
    public void TestValidRoute() {
        List<string?> route = ["api", "leaderboard"];
        var method = HttpMethod.Get;
        var result = Routes.Route(new HttpRequest { Method = method, Path = route });
        Assert.That(result, Is.TypeOf<LeaderboardEndpoint>());
    }

    [Test]
    public void TestInvalidRoute() {
        List<string?> route = ["api", "invalidroute", null];
        var method = HttpMethod.Get;
        var result = Routes.Route(new HttpRequest { Method = method, Path = route });
        Assert.That(result, Is.TypeOf<BadRequest>());
    }

    [Test]
    public void TestInvalidMethodOnRoute() {
        List<string?> route = ["api", "leaderboard"];
        var method = HttpMethod.Delete;
        var result = Routes.Route(new HttpRequest { Method = method, Path = route });
        Assert.That(result, Is.TypeOf<BadRequest>());
    }
}