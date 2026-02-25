using MediaRatingsPlatform.BusinessLayer;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;
using NSubstitute;

namespace MediaRatingsPlatform.Test;

[TestFixture]
public class AuthenticatorTests {
    [SetUp]
    public void SetUp() {
        _factory = Substitute.For<IRepositoryFactory>();
        _userRepo = Substitute.For<IUserRepository>();
        _tokenRepo = Substitute.For<ITokenRepository>();

        _factory.CreateUserRepository().Returns(_userRepo);
        _factory.CreateTokenRepository().Returns(_tokenRepo);

        _sut = new Authenticator(_factory);
    }

    private IRepositoryFactory _factory = null!;
    private IUserRepository _userRepo = null!;
    private ITokenRepository _tokenRepo = null!;
    private Authenticator _sut = null!;

    [Test]
    public void Register_WhenUserExists_Throws() {
        var input = new User { Username = "u", Password = "p" };
        _userRepo.GetUser(input).Returns(new User { Id = 1 });

        Assert.Throws<ApiUserAlreadyExistsException>(() => _sut.Register(input));
        _userRepo.DidNotReceive().AddUser(Arg.Any<User>());
    }

    [Test]
    public void Register_WhenUserDoesNotExist_AddsUser() {
        var input = new User { Username = "u", Password = "p" };
        _userRepo.GetUser(input).Returns((User?)null);

        _sut.Register(input);

        _userRepo.Received(1).AddUser(input);
    }

    [Test]
    public void Login_WhenUserNotFound_Throws() {
        var input = new User { Username = "u", Password = "p" };
        _userRepo.GetUser(input).Returns((User?)null);

        Assert.Throws<ApiBadLoginDataException>(() => _sut.Login(input));
    }

    [Test]
    public void Login_WhenPasswordMismatch_Throws() {
        var input = new User { Username = "u", Password = "p" };
        _userRepo.GetUser(input).Returns(new User { Id = 7, Username = "u", Password = "other" });

        Assert.Throws<ApiBadLoginDataException>(() => _sut.Login(input));
    }

    [Test]
    public void Login_WhenValidTokenExists_ExtendsTokenAndReturnsSameId() {
        var input = new User { Username = "u", Password = "p" };
        var existingToken = new Token {
            Id = "existing",
            UserId = 7,
            ValidUntil = DateTime.Now.AddMinutes(5)
        };

        _userRepo.GetUser(input).Returns(new User { Id = 7, Username = "u", Password = "p" });
        _tokenRepo.GetToken(Arg.Is<Token>(t => t.UserId == 7)).Returns(existingToken);

        var result = _sut.Login(input);

        Assert.That(result, Is.EqualTo("existing"));
        _tokenRepo.Received(1).UpdateToken(Arg.Is<Token>(t =>
            t.Id == "existing" &&
            t.ValidUntil > DateTime.Now.AddHours(3.5)
        ));
        _tokenRepo.DidNotReceive().AddToken(Arg.Any<Token>());
    }

    [Test]
    public void Login_WhenTokenExpired_DeletesAndCreatesNewToken() {
        var input = new User { Username = "u", Password = "p" };
        var expiredToken = new Token {
            Id = "expired",
            UserId = 7,
            ValidUntil = DateTime.Now.AddMinutes(-1)
        };

        _userRepo.GetUser(input).Returns(new User { Id = 7, Username = "u", Password = "p" });
        _tokenRepo.GetToken(Arg.Is<Token>(t => t.UserId == 7)).Returns(expiredToken);

        // Any lookup by generated Id should return null to end the loop quickly
        _tokenRepo.GetToken(Arg.Is<Token>(t => t.Id != null)).Returns((Token?)null);

        Token? addedToken = null;
        _tokenRepo.When(x => x.AddToken(Arg.Any<Token>()))
            .Do(ci => addedToken = ci.Arg<Token>());

        var result = _sut.Login(input);

        _tokenRepo.Received(1).DeleteToken(expiredToken);
        _tokenRepo.Received(1).AddToken(Arg.Any<Token>());

        Assert.That(addedToken, Is.Not.Null);
        Assert.That(addedToken!.Id, Is.Not.Null.And.Not.Empty);
        Assert.That(addedToken.UserId, Is.EqualTo(7));
        Assert.That(addedToken.ValidUntil, Is.GreaterThan(DateTime.Now.AddHours(3.5)));
        Assert.That(result, Is.EqualTo(addedToken.Id));
    }
}
