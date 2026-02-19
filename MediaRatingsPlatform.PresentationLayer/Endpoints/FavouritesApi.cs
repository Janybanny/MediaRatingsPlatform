using MediaRatingsPlatform.BusinessLayer.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class UnFavouriteEndpoint(IDependencies dependencies) : SimpleAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        var favourite = new Favourite { MediaId = request.PathId, UserId = UserId };
        dependencies.GetFavouriteManager().UnFavourite(favourite);
        return new HttpResponse {
            StatusCode = HttpStatusCode.NoContent
        };
    }
}

public class FavouriteEndpoint(IDependencies dependencies) : SimpleAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        var favourite = new Favourite { MediaId = request.PathId, UserId = UserId };
        dependencies.GetFavouriteManager().Favourite(favourite);
        return new HttpResponse {
            StatusCode = HttpStatusCode.NoContent
        };
    }
}
