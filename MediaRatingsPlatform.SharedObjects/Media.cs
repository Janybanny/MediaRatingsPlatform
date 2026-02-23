namespace MediaRatingsPlatform.SharedObjects;

public class Media : IModel {
    public int? Id { get; set; }
    public int? Owner { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? MediaType { get; set; }
    public int? ReleaseYear { get; set; }
    public List<string?>? Genres { get; set; }
    public int? AgeRestriction { get; set; }
    public List<Rating>? Ratings { get; set; }
    public float? AverageRating { get; set; }
}
