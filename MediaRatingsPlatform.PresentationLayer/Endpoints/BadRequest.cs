using MediaRatingsPlatform.BusinessLayer.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class BadRequest : NoAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.BadRequest
        };
    }
}
