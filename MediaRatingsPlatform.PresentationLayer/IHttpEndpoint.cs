using MediaRatingsPlatform.Authentication;

namespace MediaRatingsPlatform.PresentationLayer;

public interface IHttpEndpoint : IAuth {
    HttpResponse Handle(HttpRequest request);
}
