using MediaRatingsPlatform.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class ListMediaEndpoint(IDependencies dependencies) : SimpleAuth, IHttpEndpoint {
    private IDependencies _dependencies = dependencies;

    public HttpResponse Handle(HttpRequest request) {
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

public class DisplayMediaEndpoint(IDependencies dependencies) : SimpleAuth, IHttpEndpoint {
    private IDependencies _dependencies = dependencies;

    public HttpResponse Handle(HttpRequest request) {
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

public class CreateMediaEndpoint(IDependencies dependencies) : SimpleAuth, IHttpEndpoint {
    private IDependencies _dependencies = dependencies;

    public HttpResponse Handle(HttpRequest request) {
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

public class DeleteMediaEndpoint(IDependencies dependencies) : MediaAuth, IHttpEndpoint {
    private IDependencies _dependencies = dependencies;

    public HttpResponse Handle(HttpRequest request) {
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

public class UpdateMediaEndpoint(IDependencies dependencies) : MediaAuth, IHttpEndpoint {
    private IDependencies _dependencies = dependencies;

    public HttpResponse Handle(HttpRequest request) {
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
