namespace MediaRatingsPlatform.SharedObjects;

public abstract class ApiException(HttpStatusCode statusCode) : Exception {
    public HttpStatusCode StatusCode { get; } = statusCode;
}

public class ApiNotImplementedException() : ApiException(HttpStatusCode.NotImplemented) { }

public class ApiKeyMissingException() : ApiException(HttpStatusCode.BadRequest) { }

public class ApiUserAlreadyExistsException() : ApiException(HttpStatusCode.Conflict) { }

public class ApiNoAccessException() : ApiException(HttpStatusCode.Unauthorized) { }

public class ApiItemDoesNotExistException() : ApiException(HttpStatusCode.NotFound) { }

public class ApiBadLoginDataException() : ApiException(HttpStatusCode.Unauthorized) { }

public class ApiTokenExpiredException() : ApiException(HttpStatusCode.Unauthorized) { }

public class ApiDatabaseException() : ApiException(HttpStatusCode.InternalServerError) { }
