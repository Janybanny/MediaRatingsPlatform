using System.Text.Json;
using MediaRatingsPlatform.BusinessLayer.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class DisplayProfileEndpoint(IDependencies dependencies) : UserAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        var userdata = new User {
            Id = UserId
        };
        userdata = dependencies.GetUserManager().GetProfile(userdata);
        if (userdata == null)
            return new HttpResponse {
                StatusCode = HttpStatusCode.NotFound
            };
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = JsonSerializer.Serialize(userdata)
        };
    }
}

public class UpdateProfileEndpoint(IDependencies dependencies) : UserAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        var userdata = request.Body != null ? JsonSerializer.Deserialize<User>(request.Body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) : null;
        if (userdata == null)
            return new HttpResponse {
                StatusCode = HttpStatusCode.BadRequest
            };
        userdata.Id = UserId;
        dependencies.GetUserManager().UpdateProfile(userdata);
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok
        };
    }
}

public class DisplayRatingsEndpoint(IDependencies dependencies) : UserAuth, IHttpEndpoint {
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
    public HttpResponse Handle(HttpRequest request) {
        var favourites = dependencies.GetFavouriteManager().GetFavourites(new User {Id = UserId});
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = JsonSerializer.Serialize(favourites)
        };
    }
}
