using System.Text.Json;
using MediaRatingsPlatform.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class LoginEndpoint(IDependencies dependencies) : NoAuth, IHttpEndpoint {
    private readonly IDependencies _dependencies = dependencies;

    public HttpResponse Handle(HttpRequest request) {
        // check data
        var logInData = request.Body != null ? JsonSerializer.Deserialize<User>(request.Body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) : null;
        if (logInData?.Username == null || logInData.Password == null) throw new ApiKeyMissingException();

        var token = _dependencies.GetAuth().Login(logInData);
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = token
        };
    }
}

public class RegisterEndpoint(IDependencies dependencies) : NoAuth, IHttpEndpoint {
    private readonly IDependencies _dependencies = dependencies;

    public HttpResponse Handle(HttpRequest request) {
        // check data
        var logInData = request.Body != null ? JsonSerializer.Deserialize<User>(request.Body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) : null;
        if (logInData?.Username == null || logInData.Password == null) throw new ApiKeyMissingException();

        _dependencies.GetAuth().Register(logInData);
        return new HttpResponse {
            StatusCode = HttpStatusCode.Created
        };
    }
}
