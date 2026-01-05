using MediaRatingsPlatform.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class UnFavouriteEndpoint : UserAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        throw new ApiNotImplementedException();

        //success
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok
        };
    }
}

public class FavouriteEndpoint : UserAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        throw new ApiNotImplementedException();

        //success
        return new HttpResponse {
            StatusCode = HttpStatusCode.NoContent
        };
    }
}
