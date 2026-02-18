using MediaRatingsPlatform.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class DisplayProfileEndpoint(IDependencies dependencies) : UserAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.NotImplemented,
            Body = "DEBUG: DisplayProfile"
        };
        //success
        var user = ""; // includes user profile
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = user
        };
    }
}

public class UpdateProfileEndpoint(IDependencies dependencies) : UserAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.NotImplemented,
            Body = "DEBUG: UpdateProfile"
        };
        //success
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok
        };
    }
}

public class DisplayRatingsEndpoint(IDependencies dependencies) : UserAuth, IHttpEndpoint {
    private IDependencies _dependencies = dependencies;

    public HttpResponse Handle(HttpRequest request) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.NotImplemented,
            Body = "DEBUG: DisplayRatings"
        };
        //success
        var ratings = ""; // includes user ratings
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = ratings
        };
    }
}

public class DisplayFavouritesEndpoint(IDependencies dependencies) : UserAuth, IHttpEndpoint {
    private IDependencies _dependencies = dependencies;

    public HttpResponse Handle(HttpRequest request) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.NotImplemented,
            Body = "DEBUG: DisplayFavourites"
        };
        //success
        var favourites = ""; // includes user favourites
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = favourites
        };
    }
}
