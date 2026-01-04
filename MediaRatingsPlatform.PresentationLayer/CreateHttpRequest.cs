using System.Collections.Specialized;
using System.Net;

namespace MediaRatingsPlatform.PresentationLayer;

public class CreateHttpRequest
{
    public static HttpRequest CreateRequest(IHttpListenerRequest request) {
        var formattedRequest = new HttpRequest();
        Enum.TryParse(request.HttpMethod, out HttpMethod requestMethod);
        formattedRequest.Method = requestMethod;
        formattedRequest.Path = request.Url?.AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries).ToList()!;
        formattedRequest.QueryParams = request.QueryString.AllKeys
            .Where(key => !string.IsNullOrWhiteSpace(key))
            .ToDictionary(key => key!, key => request.QueryString[key] ?? "");
        formattedRequest.Headers = request.Headers.AllKeys
            .Where(key => !string.IsNullOrWhiteSpace(key))
            .ToDictionary(key => key!, key => request.Headers[key] ?? "");
        formattedRequest.Body = new StreamReader(request.InputStream).ReadToEnd();
        for (int i = 0; i < formattedRequest.Path.Count; i++)
        {
            if (!int.TryParse(formattedRequest.Path[i], out _)) continue;
            if (formattedRequest.PathId == "")
            {
                formattedRequest.PathId = formattedRequest.Path[i]!;
                formattedRequest.Path[i] = null;
            }
            else
            {
                formattedRequest.Path = new List<string?>();
                break;
            }
        }
        return formattedRequest;
    }
}

public interface IHttpListenerRequest
{
    public string HttpMethod  { get; }
    public Uri? Url { get; }
    public NameValueCollection QueryString { get;  }
    public NameValueCollection Headers { get;  }
    public Stream InputStream { get; }
}

public class HttpListenerRequestWrapper(HttpListenerRequest listenerRequest) :  IHttpListenerRequest
{
    private readonly HttpListenerRequest _listenerRequest = listenerRequest;
    public string HttpMethod => _listenerRequest.HttpMethod;
    public Uri? Url => _listenerRequest.Url;
    public NameValueCollection QueryString => _listenerRequest.QueryString;
    public NameValueCollection Headers => _listenerRequest.Headers;
    public Stream InputStream => _listenerRequest.InputStream;
}