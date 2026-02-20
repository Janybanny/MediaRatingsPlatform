using MediaRatingsPlatform.BusinessLayer.Interfaces;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer;

public class FavouriteManager(IRepositoryFactory database) : IFavouriteManager {
    public void Favourite(Favourite favourite) {
        if (database.CreateMediaRepository().GetMedia(new Media { Id = favourite.MediaId }) == null) throw new ApiItemDoesNotExistException();
        database.CreateFavouriteRepository().Favourite(favourite);
    }

    public void UnFavourite(Favourite favourite) {
        if (database.CreateMediaRepository().GetMedia(new Media { Id = favourite.MediaId }) == null) throw new ApiItemDoesNotExistException();
        database.CreateFavouriteRepository().Unfavourite(favourite);
    }

    public List<Media> GetFavourites(User user) {
        var favourites = database.CreateFavouriteRepository().GetFavourites(user);
        List<Media> results = [];
        var db = database.CreateMediaRepository();
        foreach (var favourite in favourites) results.Add(db.GetMedia(new Media { Id = favourite.MediaId })!);
        return results;
    }
}
