using MediaRatingsPlatform.Authentication;
using MediaRatingsPlatform.Models;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class LoginEndpoint : NoAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request, string username)
    {
        User logInData; // TODO Serialize json :)
        try
        {
            logInData = new User { Username = request.QueryParams["username"], Password = request.QueryParams["password"] };
        }
        catch (KeyNotFoundException)
        {
            throw new ApiKeyNotFoundException();
        }

        Login login = new Login();
        string token = login.Execute(logInData);
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = token
        };
    }
}

public class RegisterEndpoint : NoAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request, string username) {
        User logInData = new User { Username = request.QueryParams["username"], Password = request.QueryParams["password"] };
        Register register = new Register();
        register.Execute(logInData);
        return new HttpResponse {
            StatusCode = HttpStatusCode.Created
        };
    }
}
