using System.Collections.Specialized;
using System.Text;
using MediaRatingsPlatform.PresentationLayer;
using NSubstitute;
using HttpMethod = MediaRatingsPlatform.PresentationLayer.HttpMethod;

namespace MediaRatingsPlatform.Test;

public class HttpRequestsParsingTest {
    [Test]
    public void TestValidCreateHttpRequest() {
        var request = Substitute.For<IHttpListenerRequest>();
        request.HttpMethod.Returns("GET");
        request.Url.Returns(new Uri("http://localhost:9080/api/media/2/rate/"));
        request.QueryString.Returns(new NameValueCollection { { "key", "value" } });
        request.Headers.Returns(new NameValueCollection
            { { "Authorization", "Bearer testtoken" }, { "testkey", "testvalue" } });
        var byteArray = Encoding.ASCII.GetBytes("TESTBODY");
        request.InputStream.Returns(new MemoryStream(byteArray));

        var result = CreateHttpRequest.CreateRequest(request);

        Assert.That(result.Path, Is.EqualTo(["api", "media", null, "rate"]));
        Assert.That(result.PathId, Is.EqualTo(2));
        Assert.That(result.Method, Is.EqualTo(HttpMethod.Get));
        Assert.That(result.Body, Is.EqualTo("TESTBODY"));
        Assert.That(result.Headers,
            Is.EqualTo(new Dictionary<string, string>
                { { "Authorization", "Bearer testtoken" }, { "testkey", "testvalue" } }));
        Assert.That(result.QueryParams, Is.EqualTo(new Dictionary<string, string> { { "key", "value" } }));
        Assert.That(result.GetToken(), Is.EqualTo("testtoken"));
        Assert.That(result.GetComparator(), Is.EqualTo(2));
    }

    [Test]
    public void TestEdgeCaseCreateHttpRequest() {
        // no bearer token
        // no comparator (aka no id in path)
        var request = Substitute.For<IHttpListenerRequest>();
        request.HttpMethod.Returns("get");
        request.Url.Returns(new Uri("http://localhost:9080/"));
        request.QueryString.Returns(new NameValueCollection());
        request.Headers.Returns(new NameValueCollection { { "Authorization", "Bearrer testtoken" } });
        var byteArray = Encoding.ASCII.GetBytes("{body:\ntest]");
        request.InputStream.Returns(new MemoryStream(byteArray));

        var result = CreateHttpRequest.CreateRequest(request);

        Assert.That(result.Path, Is.EqualTo(new List<string>()));
        Assert.That(result.PathId, Is.EqualTo(null));
        Assert.That(result.Method, Is.EqualTo(HttpMethod.Get));
        Assert.That(result.Body, Is.EqualTo("{body:\ntest]"));
        Assert.That(result.Headers,
            Is.EqualTo(new Dictionary<string, string> { { "Authorization", "Bearrer testtoken" } }));
        Assert.That(result.QueryParams, Is.EqualTo(new Dictionary<string, string>()));
        Assert.That(result.GetToken(), Is.EqualTo(""));
        Assert.That(result.GetComparator(), Is.EqualTo(null));
    }

    [Test]
    public void TestNoAuthHeaderDoubleUrlKeyCreateHttpRequest() {
        // no bearer token
        // no comparator (aka no id in path)
        var request = Substitute.For<IHttpListenerRequest>();
        request.HttpMethod.Returns("get");
        request.Url.Returns(new Uri("http://localhost:9080/api/fish/0/1/2"));
        request.QueryString.Returns(new NameValueCollection());
        request.Headers.Returns(new NameValueCollection { { "one", "two" } });
        var byteArray = Encoding.ASCII.GetBytes("");
        request.InputStream.Returns(new MemoryStream(byteArray));

        var result = CreateHttpRequest.CreateRequest(request);

        Assert.That(result.Path, Is.EqualTo(new List<string>()));
        Assert.That(result.PathId, Is.EqualTo(0));
        Assert.That(result.Method, Is.EqualTo(HttpMethod.Get));
        Assert.That(result.Body, Is.EqualTo(""));
        Assert.That(result.Headers, Is.EqualTo(new Dictionary<string, string> { { "one", "two" } }));
        Assert.That(result.QueryParams, Is.EqualTo(new Dictionary<string, string>()));
        Assert.That(result.GetToken(), Is.EqualTo(""));
        Assert.That(result.GetComparator(), Is.EqualTo(0));
    }
}
