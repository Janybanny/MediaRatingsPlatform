using MediaRatingsPlatform.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class ListMediaEndpoint : SimpleAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request, string username) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.NotImplemented,
            Body = "DEBUG: ListMedia"
        };
        //success
        var medias = ""; // includes requested list of media
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = medias
        };
    }
}

public class DisplayMediaEndpoint : SimpleAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request, string username) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.NotImplemented,
            Body = "DEBUG: DisplayMedia"
        };
        //success
        var media = ""; // includes requested list of media
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = media
        };
    }
}

public class CreateMediaEndpoint : SimpleAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request, string username) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.NotImplemented,
            Body = "DEBUG: CreateMedia"
        };
        //success
        return new HttpResponse {
            StatusCode = HttpStatusCode.Created
        };
    }
}

public class DeleteMediaEndpoint : MediaAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request, string username) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.NotImplemented,
            Body = "DEBUG: DeleteMedia"
        };
        //success
        return new HttpResponse {
            StatusCode = HttpStatusCode.NoContent
        };
    }
}

public class UpdateMediaEndpoint : MediaAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request, string username) {
        return new HttpResponse {
            StatusCode = HttpStatusCode.NotImplemented,
            Body = "DEBUG: UpdateMedia"
        };
        //success
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok
        };
    }
}