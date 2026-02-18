using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.Interfaces;

namespace MediaRatingsPlatform.PresentationLayer;

public interface IDependencies {
    public IRepositoryFactory GetDatabase();
    public IAuthenticator GetAuth();
}
