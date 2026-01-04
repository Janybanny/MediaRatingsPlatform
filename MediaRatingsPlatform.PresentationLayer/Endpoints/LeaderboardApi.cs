using MediaRatingsPlatform.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class LeaderboardEndpoint : SimpleAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request, string username) {
        //SUCCESS
        var returnvalue = ""; // includes leaderboard list
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = returnvalue
        };
    }
}