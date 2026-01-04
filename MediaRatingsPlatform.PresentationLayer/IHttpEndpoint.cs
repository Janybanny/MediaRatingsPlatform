using MediaRatingsPlatform.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer;

public interface IHttpEndpoint : IAuth {
    new string Auth(Token token, string comparator);
    HttpResponse Handle(HttpRequest request, string username);
}