using System.Text.Json;
using MediaRatingsPlatform.BusinessLayer.Authentication;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.PresentationLayer.Endpoints;

public class RecommendEndpoint(IDependencies dependencies) : SimpleAuth, IHttpEndpoint {
    public HttpResponse Handle(HttpRequest request) {
        var type = request.Body != null ? JsonSerializer.Deserialize<string>(request.Body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) : null;
        List<Media> recommendations;
        if (type == "genre")
            recommendations = dependencies.GetRecommendationManager().GetRecommendationsByGenre(new User { Id = request.PathId }, dependencies.GetMediaManager());
        else if (type == "content")
            recommendations = dependencies.GetRecommendationManager().GetRecommendationsByContent(new User { Id = request.PathId }, dependencies.GetMediaManager());
        else
            return new HttpResponse {
                StatusCode = HttpStatusCode.BadRequest
            };
        return new HttpResponse {
            StatusCode = HttpStatusCode.Ok,
            Body = JsonSerializer.Serialize(recommendations)
        };
    }
}
