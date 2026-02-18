using MediaRatingsPlatform.PresentationLayer.Endpoints;

namespace MediaRatingsPlatform.PresentationLayer;

public abstract class Routes {
    public static IHttpEndpoint Route(HttpRequest request, IDependencies dependencies) {
        return request switch {
            { Method: HttpMethod.Get, Path: ["api", "leaderboard"] } => new LeaderboardEndpoint(dependencies),
            { Method: HttpMethod.Get, Path: ["api", "media"] } => new ListMediaEndpoint(dependencies),
            { Method: HttpMethod.Post, Path: ["api", "media"] } => new CreateMediaEndpoint(dependencies),
            { Method: HttpMethod.Get, Path: ["api", "media", null] } => new DisplayMediaEndpoint(dependencies),
            { Method: HttpMethod.Put, Path: ["api", "media", null] } => new UpdateMediaEndpoint(dependencies),
            { Method: HttpMethod.Delete, Path: ["api", "media", null] } => new DeleteMediaEndpoint(dependencies),
            { Method: HttpMethod.Post, Path: ["api", "media", null, "favorite"] } => new FavouriteEndpoint(dependencies),
            { Method: HttpMethod.Delete, Path: ["api", "media", null, "favorite"] } => new UnFavouriteEndpoint(dependencies),
            { Method: HttpMethod.Post, Path: ["api", "media", null, "rate"] } => new CreateRatingEndpoint(dependencies),
            { Method: HttpMethod.Put, Path: ["api", "ratings", null] } => new UpdateRatingEndpoint(dependencies),
            { Method: HttpMethod.Delete, Path: ["api", "ratings", null] } => new DeleteRatingEndpoint(dependencies),
            { Method: HttpMethod.Post, Path: ["api", "ratings", null, "confirm"] } => new ConfirmCommentEndpoint(dependencies),
            { Method: HttpMethod.Post, Path: ["api", "ratings", null, "like"] } => new LikeRatingEndpoint(dependencies),
            { Method: HttpMethod.Get, Path: ["api", "users", null, "favorites"] } => new DisplayFavouritesEndpoint(dependencies),
            { Method: HttpMethod.Get, Path: ["api", "users", null, "profile"] } => new DisplayProfileEndpoint(dependencies),
            { Method: HttpMethod.Put, Path: ["api", "users", null, "profile"] } => new UpdateProfileEndpoint(dependencies),
            { Method: HttpMethod.Get, Path: ["api", "users", null, "ratings"] } => new DisplayRatingsEndpoint(dependencies),
            { Method: HttpMethod.Get, Path: ["api", "users", null, "recommendations"] } => new RecommendEndpoint(dependencies),
            { Method: HttpMethod.Post, Path: ["api", "users", "login"] } => new LoginEndpoint(dependencies),
            { Method: HttpMethod.Post, Path: ["api", "users", "register"] } => new RegisterEndpoint(dependencies),
            _ => new BadRequest()
        };
    }
}
