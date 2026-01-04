namespace MediaRatingsPlatform.SharedObjects;

public class User : IModel {
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? FavouriteGenre { get; set; }
}
