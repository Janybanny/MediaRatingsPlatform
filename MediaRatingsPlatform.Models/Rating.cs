namespace MediaRatingsPlatform.Models;

public class Rating
{
    public int? Id { get; set; }
    public string? Owner { get; set; }
    public int? MediaId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int? Stars { get; set; }
    public string? Comment { get; set; }
    public bool? CommentVisible { get; set; }
}