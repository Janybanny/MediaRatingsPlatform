using System.Text.Json;
using MediaRatingsPlatform.BusinessLayer.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class CreateRatingEndpoint(IDependencies dependencies) : SimpleAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        var rating = request.Body != null ? JsonSerializer.Deserialize<Rating>(request.Body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) : null;
        if (rating == null || rating.Stars < 1 || rating.Stars > 5)
            return new HttpResponse {
                StatusCode = HttpStatusCode.BadRequest
            };
        rating.Owner = UserId;
        rating.MediaId = request.PathId;
        dependencies.GetRatingManager().RateMedia(rating);
        return new HttpResponse {
            StatusCode = HttpStatusCode.Created
        };
    }
}

public class LikeRatingEndpoint(IDependencies dependencies) : SimpleAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        dependencies.GetRatingManager().LikeRating(new Like { RatingId = request.PathId, UserId = UserId });
        return new HttpResponse {
            StatusCode = HttpStatusCode.NoContent
        };
    }
}

public class UpdateRatingEndpoint(IDependencies dependencies) : RatingAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        var rating = request.Body != null ? JsonSerializer.Deserialize<Rating>(request.Body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) : null;
        if (rating == null || rating.Stars < 1 || rating.Stars > 5)
            return new HttpResponse {
                StatusCode = HttpStatusCode.BadRequest
            };
        rating.Id = request.PathId;
        dependencies.GetRatingManager().UpdateRating(rating);
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok
        };
    }
}

public class ConfirmCommentEndpoint(IDependencies dependencies) : RatingAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        dependencies.GetRatingManager().ComfirmRatingComment(new Rating { Id = request.PathId });
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok
        };
    }
}

public class DeleteRatingEndpoint(IDependencies dependencies) : RatingAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        dependencies.GetRatingManager().DeleteRating(new Rating { Id = request.PathId });
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok
        };
    }
}
