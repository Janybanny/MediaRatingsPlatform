namespace MediaRatingsPlatform.SharedObjects;

public class Rating : IModel {
    public int? Id { get; set; }
    public int? Owner { get; set; }
    public int? MediaId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? Stars { get; set; }
    public string? Comment { get; set; }
    public bool? CommentVisible { get; set; }
}
