namespace MediaRatingsPlatform.SharedObjects;

public class User : IModel {
    public int? Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? EMail { get; set; }
    public string? FavouriteGenre { get; set; }
}
