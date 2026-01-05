using MediaRatingsPlatform.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class DisplayProfileEndpoint : UserAuth, IHttpEndpoint {
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

public class UpdateProfileEndpoint : UserAuth, IHttpEndpoint {
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

public class DisplayRatingsEndpoint : UserAuth, IHttpEndpoint {
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

public class DisplayFavouritesEndpoint : UserAuth, IHttpEndpoint {
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
