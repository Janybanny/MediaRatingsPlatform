namespace MediaRatingsPlatform.SharedObjects;

public class Media : IModel {
    public int? Id { get; set; }
    public string? Owner { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public MediaType? MediaType { get; set; }
    public int? ReleaseYear { get; set; }
    public string[]? Genres { get; set; }
    public int? AgeRestriction { get; set; }
}

public enum MediaType {
    Movie,
    Series,
    Game
}