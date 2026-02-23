using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer.Interfaces;

public interface IMediaManager {
    public Media? GetMedia(Media media, int userId);
    public void AddMedia(Media media);
    public void UpdateMedia(Media media);
    public void DeleteMedia(Media media);
    public IEnumerable<Media>? ListMedias(Media filter);
}
