using MediaRatingsPlatform.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class UnFavouriteEndpoint(IDependencies dependencies) : UserAuth, IHttpEndpoint {
    private IDependencies _dependencies = dependencies;

    public HttpResponse Handle(HttpRequest request) {
        throw new ApiNotImplementedException();

        //success
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok
        };
    }
}

public class FavouriteEndpoint(IDependencies dependencies) : UserAuth, IHttpEndpoint {
    private IDependencies _dependencies = dependencies;

    public HttpResponse Handle(HttpRequest request) {
        throw new ApiNotImplementedException();

        //success
        return new HttpResponse {
            StatusCode = HttpStatusCode.NoContent
        };
    }
}
