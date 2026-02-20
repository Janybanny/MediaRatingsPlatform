using MediaRatingsPlatform.BusinessLayer.Interfaces;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer;

public class MediaManager(IRepositoryFactory database) : IMediaManager {
    public Media? GetMedia(Media media) {
        return database.CreateMediaRepository().GetMedia(media);
    }

    public IEnumerable<Media> ListMedias(Media filter) {
        throw new ApiNotImplementedException();
    }

    public void AddMedia(Media media) {
        database.CreateMediaRepository().AddMedia(media);
    }

    public void UpdateMedia(Media media) {
        database.CreateMediaRepository().UpdateMedia(media);
    }

    public void DeleteMedia(Media media) {
        database.CreateMediaRepository().DeleteMedia(media);
    }
}
