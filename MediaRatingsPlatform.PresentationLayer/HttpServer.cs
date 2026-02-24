using System.Net;
using System.Text;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer;

public class HttpServer {
    private readonly IDependencies _dependencies;
    private readonly HttpListener _listener;
    private bool _stopServer;

    public HttpServer(string prefix, IDependencies dependencies) {
        _dependencies = dependencies;
        _listener = new HttpListener();
        _listener.Prefixes.Add(prefix);
    }

    public void StartServer() {
        _listener.Start();
        while (!_stopServer) {
            var context = _listener.GetContext();
            // Parses Request
            var request = CreateHttpRequest.CreateRequest(new HttpListenerRequestWrapper(context.Request));
            // Routes Request and Creates Endpoint
            var endpoint = Routes.Route(request, _dependencies);
            // Fulfills request and catches errors, which return API Errors
            HttpResponse response;
            try {
                endpoint.Auth(new Token { Id = request.GetToken() }, request.GetComparator(), _dependencies.GetDatabase());
                response = endpoint.Handle(request);
            } catch (ApiException ex) {
                response = new HttpResponse { StatusCode = ex.StatusCode };
            }
            Respond(response, context.Response);
        }
        _listener.Stop();
    }

    public void StopServer() {
        _stopServer = true;
    }

    private static void Respond(HttpResponse response, HttpListenerResponse formattedResponse) {
        formattedResponse.StatusCode = (int)response.StatusCode;
        if (!string.IsNullOrEmpty(response.Body)) {
            var encoding = formattedResponse.ContentEncoding ?? Encoding.UTF8;
            formattedResponse.ContentEncoding = encoding;
            var buffer = encoding.GetBytes(response.Body);
            formattedResponse.ContentLength64 = buffer.Length;
            formattedResponse.OutputStream.Write(buffer, 0, buffer.Length);
        }

        formattedResponse.Close();
    }
}
