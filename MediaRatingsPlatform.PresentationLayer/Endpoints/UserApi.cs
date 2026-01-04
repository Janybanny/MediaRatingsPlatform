using System.Reflection.Metadata;
using MediaRatingsPlatform.Authentication;
using MediaRatingsPlatform.Models;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class DisplayProfileEndpoint : UserAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request, string username) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.NotImplemented,
            Body = "DEBUG: DisplayProfile"
        };
        //success
        string user = ""; // includes user profile
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = user
        };
    }
}

public class UpdateProfileEndpoint : UserAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request, string username) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.NotImplemented,
            Body = "DEBUG: UpdateProfile"
        };
        //success
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
        };
    }
}

public class DisplayRatingsEndpoint : UserAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request, string username) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.NotImplemented,
            Body = "DEBUG: DisplayRatings"
        };
        //success
        string ratings = ""; // includes user ratings
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = ratings
        };
    }
}

public class DisplayFavouritesEndpoint : UserAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request, string username) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.NotImplemented,
            Body = "DEBUG: DisplayFavourites"
        };
        //success
        string favourites = ""; // includes user favourites
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = favourites
        };
    }
}
