using MediaRatingsPlatform.BusinessLayer.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class LeaderboardEndpoint(IDependencies dependencies) : SimpleAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        //SUCCESS
        var returnvalue = ""; // includes leaderboard list
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = returnvalue
        };
    }
}
