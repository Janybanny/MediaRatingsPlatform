using System.Diagnostics.CodeAnalysis;

namespace MediaRatingsPlatform.PresentationLayer;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum HttpMethod
{
    Get,
    Post,
    Put,
    Delete,
    GET = Get,
    POST = Post,
    PUT = Put,
    DELETE = Delete,
    get = Get,
    post = Post,
    put = Put,
    delete = Delete,
}
