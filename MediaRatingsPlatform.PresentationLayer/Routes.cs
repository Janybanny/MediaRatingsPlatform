using MediaRatingsPlatform.PresentationLayer.Endpoints;

namespace MediaRatingsPlatform.PresentationLayer;

public abstract class Routes {
    public static IHttpEndpoint Route(HttpRequest request) {
        return request switch {
            { Method: HttpMethod.Get, Path: ["api", "leaderboard"] } => new LeaderboardEndpoint(),
            { Method: HttpMethod.Get, Path: ["api", "media"] } => new ListMediaEndpoint(),
            { Method: HttpMethod.Post, Path: ["api", "media"] } => new CreateMediaEndpoint(),
            { Method: HttpMethod.Get, Path: ["api", "media", null] } => new DisplayMediaEndpoint(),
            { Method: HttpMethod.Put, Path: ["api", "media", null] } => new UpdateMediaEndpoint(),
            { Method: HttpMethod.Delete, Path: ["api", "media", null] } => new DeleteMediaEndpoint(),
            { Method: HttpMethod.Post, Path: ["api", "media", null, "favorite"] } => new FavouriteEndpoint(),
            { Method: HttpMethod.Delete, Path: ["api", "media", null, "favorite"] } => new UnFavouriteEndpoint(),
            { Method: HttpMethod.Post, Path: ["api", "media", null, "rate"] } => new CreateRatingEndpoint(),
            { Method: HttpMethod.Put, Path: ["api", "ratings", null] } => new UpdateRatingEndpoint(),
            { Method: HttpMethod.Delete, Path: ["api", "ratings", null] } => new DeleteRatingEndpoint(),
            { Method: HttpMethod.Post, Path: ["api", "ratings", null, "confirm"] } => new ConfirmCommentEndpoint(),
            { Method: HttpMethod.Post, Path: ["api", "ratings", null, "like"] } => new LikeRatingEndpoint(),
            { Method: HttpMethod.Get, Path: ["api", "users", null, "favorites"] } => new DisplayFavouritesEndpoint(),
            { Method: HttpMethod.Get, Path: ["api", "users", null, "profile"] } => new DisplayProfileEndpoint(),
            { Method: HttpMethod.Put, Path: ["api", "users", null, "profile"] } => new UpdateProfileEndpoint(),
            { Method: HttpMethod.Get, Path: ["api", "users", null, "ratings"] } => new DisplayRatingsEndpoint(),
            { Method: HttpMethod.Get, Path: ["api", "users", null, "recommendations"] } => new RecommendEndpoint(),
            { Method: HttpMethod.Post, Path: ["api", "users", "login"] } => new LoginEndpoint(),
            { Method: HttpMethod.Post, Path: ["api", "users", "register"] } => new RegisterEndpoint(),
            _ => new BadRequest()
        };
    }
}