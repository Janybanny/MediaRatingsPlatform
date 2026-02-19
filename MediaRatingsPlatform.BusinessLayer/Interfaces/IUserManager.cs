using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer.Interfaces;

public interface IUserManager {
    public User? GetProfile(User input);
    public void UpdateProfile(User input);
}
