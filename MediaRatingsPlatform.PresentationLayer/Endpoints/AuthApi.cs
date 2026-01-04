using System.Text.Json;
using MediaRatingsPlatform.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class LoginEndpoint : NoAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request, string username) {
        // check data
        User? logInData = request.Body != null ? JsonSerializer.Deserialize<User>(request.Body) : null;
        if (logInData?.Username == null || logInData.Password == null) throw new ApiKeyMissingException();

        var token = Authenticator.Login(logInData);
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = token
        };
    }
}

public class RegisterEndpoint : NoAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request, string username) {
        // check data
        User? logInData = request.Body != null ? JsonSerializer.Deserialize<User>(request.Body) : null;
        if (logInData?.Username == null || logInData.Password == null) throw new ApiKeyMissingException();
        
        Authenticator.Register(logInData);
        return new HttpResponse {
            StatusCode = HttpStatusCode.Created
        };
    }
}
