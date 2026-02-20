using System.Text.Json;
using MediaRatingsPlatform.BusinessLayer.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class ListMediaEndpoint(IDependencies dependencies) : SimpleAuth, IHttpEndpoint {
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
    public HttpResponse Handle(HttpRequest request) {
        var media = dependencies.GetMediaManager().GetMedia(new Media { Id = request.PathId });
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = JsonSerializer.Serialize(media)
        };
    }
}

public class CreateMediaEndpoint(IDependencies dependencies) : SimpleAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        var media = request.Body != null ? JsonSerializer.Deserialize<Media>(request.Body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) : null;
        if (media == null)
            return new HttpResponse {
                StatusCode = HttpStatusCode.BadRequest
            };
        media.Id = request.PathId;
        media.Owner = UserId;
        dependencies.GetMediaManager().AddMedia(media);
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok
        };
    }
}

public class DeleteMediaEndpoint(IDependencies dependencies) : MediaAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        dependencies.GetMediaManager().DeleteMedia(new Media { Id = request.PathId });
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok
        };
    }
}

public class UpdateMediaEndpoint(IDependencies dependencies) : MediaAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        var media = request.Body != null ? JsonSerializer.Deserialize<Media>(request.Body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) : null;
        if (media == null)
            return new HttpResponse {
                StatusCode = HttpStatusCode.BadRequest
            };
        media.Id = request.PathId;
        media.Owner = UserId;
        dependencies.GetMediaManager().UpdateMedia(media);
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok
        };
    }
}
