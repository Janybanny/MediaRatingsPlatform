using System.Text.Json;
using MediaRatingsPlatform.BusinessLayer.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class ListMediaEndpoint(IDependencies dependencies) : SimpleAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        var mediaFilter = new MediaFilter {
            Title = request.QueryParams!.GetValueOrDefault("title", null),
            Genre = request.QueryParams!.GetValueOrDefault("genre", null),
            MediaType = request.QueryParams!.GetValueOrDefault("mediaType", null),
            ReleaseYear = int.TryParse(request.QueryParams!.GetValueOrDefault("releaseYear", null), out var ry) ? ry : null,
            AgeRestriction = int.TryParse(request.QueryParams!.GetValueOrDefault("ageRestriction", null), out var ar) ? ar : null,
            Rating = int.TryParse(request.QueryParams!.GetValueOrDefault("rating", null), out var ra) ? ra : null,
            SortBy = request.QueryParams!.GetValueOrDefault("sortBy", null)
        };
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = JsonSerializer.Serialize(dependencies.GetMediaManager().ListMedias(mediaFilter, (int)UserId!))
        };
    }
}

public class DisplayMediaEndpoint(IDependencies dependencies) : SimpleAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        var media = dependencies.GetMediaManager().GetMedia(new Media { Id = request.PathId }, (int)UserId!);
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
            StatusCode = HttpStatusCode.Created
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
