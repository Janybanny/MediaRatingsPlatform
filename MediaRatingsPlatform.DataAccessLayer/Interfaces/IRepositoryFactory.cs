namespace MediaRatingsPlatform.DataAccessLayer.Interfaces;

public interface IRepositoryFactory {
    public IUserRepository CreateUserRepository();
    public ITokenRepository CreateTokenRepository();
    public IRatingRepository CreateRatingRepository();
    public IMediaRepository CreateMediaRepository();
    public IFavouriteRepository CreateFavouriteRepository();
}
