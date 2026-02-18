using MediaRatingsPlatform.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class LeaderboardEndpoint(IDependencies dependencies) : SimpleAuth, IHttpEndpoint {
    private IDependencies _dependencies = dependencies;

    public HttpResponse Handle(HttpRequest request) {
        //SUCCESS
        var returnvalue = ""; // includes leaderboard list
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = returnvalue
        };
    }
}
