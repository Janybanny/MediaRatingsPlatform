using MediaRatingsPlatform.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class BadRequest : NoAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request, string username) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.BadRequest
        };
    }
}