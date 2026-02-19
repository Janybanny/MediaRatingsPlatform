using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.BusinessLayer.Interfaces;

public interface IAuthenticator {
    public void Register(User logInData);
    public string Login(User logInData);
}
