using MediaRatingsPlatform.Authentication;
using MediaRatingsPlatform.Models;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class LeaderboardEndpoint : SimpleAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request, string username)
    {
        
        //SUCCESS
        string returnvalue = ""; // includes leaderboard list
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = returnvalue
        };
    }
}
