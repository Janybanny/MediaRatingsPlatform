using MediaRatingsPlatform.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class CreateRatingEndpoint : SimpleAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.NotImplemented,
            Body = "DEBUG: CreateRating"
        };
        //success
        return new HttpResponse {
            StatusCode = HttpStatusCode.Created
        };
    }
}

public class LikeRatingEndpoint : SimpleAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.NotImplemented,
            Body = "DEBUG: LikeRating"
        };
        //success
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok
        };
    }
}

public class UpdateRatingEndpoint : RatingAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.NotImplemented,
            Body = "DEBUG: UpdateRating"
        };
        //success
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok
        };
    }
}

public class DeleteRatingEndpoint : RatingAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.NotImplemented,
            Body = "DEBUG: DeleteRating"
        };
        //success
        return new HttpResponse {
            StatusCode = HttpStatusCode.NoContent
        };
    }
}

public class ConfirmCommentEndpoint : RatingAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.NotImplemented,
            Body = "DEBUG: ConfirmComment"
        };
        //success
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok
        };
    }
}
