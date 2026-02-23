namespace MediaRatingsPlatform.SharedObjects;

public class MediaFilter : IModel {
    public string? Title { get; set; }
    public string? Genre { get; set; }
    public string? MediaType { get; set; }
    public int? ReleaseYear { get; set; }
    public int? AgeRestriction { get; set; }
    public int? Rating { get; set; }
    public string? SortBy { get; set; }
}
