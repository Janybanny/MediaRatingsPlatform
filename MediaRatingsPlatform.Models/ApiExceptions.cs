namespace MediaRatingsPlatform.Models;

public abstract class ApiException(HttpStatusCode statusCode) : Exception
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}

public class ApiNotImplementedException() : ApiException(HttpStatusCode.NotImplemented) { }
public class ApiKeyNotFoundException() : ApiException(HttpStatusCode.BadRequest) { }
