using MediaRatingsPlatform.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class RecommendEndpoint : SimpleAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.NotImplemented,
            Body = "DEBUG: Recommend"
        };
        //success
        var recommendations = ""; // includes recommendations
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = recommendations
        };
    }
}
