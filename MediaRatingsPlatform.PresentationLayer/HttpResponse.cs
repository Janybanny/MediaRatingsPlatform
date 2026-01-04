using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer;

public class HttpResponse {
    public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.InternalServerError;
    public string? Body { get; init; }
}