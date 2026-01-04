using MediaRatingsPlatform.Authentication;

namespace MediaRatingsPlatform.PresentationLayer;

public interface IHttpEndpoint : IAuth
{
    new string Auth(string token, string comparator);
    HttpResponse Handle(HttpRequest request, string username);
}

