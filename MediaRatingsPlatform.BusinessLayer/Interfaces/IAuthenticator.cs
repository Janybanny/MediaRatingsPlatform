using MediaRatingsPlatform.SharedObjects;

namespace MediaRatingsPlatform.Interfaces;

public interface IAuthenticator {
    public void Register(User logInData);
    public string Login(User logInData);
}
