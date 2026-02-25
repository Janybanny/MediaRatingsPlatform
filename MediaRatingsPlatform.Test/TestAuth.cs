using MediaRatingsPlatform.BusinessLayer.Authentication;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;
using NSubstitute;

namespace MediaRatingsPlatform.Test;

[TestFixture]
internal class TestAuth {
    private IRepositoryFactory _factory = null!;
    private ITokenRepository _tokenRepo = null!;
    private IMediaRepository _mediaRepo = null!;
    private IRatingRepository _ratingRepo = null!;

    [SetUp]
    public void SetUp() {
        _factory = Substitute.For<IRepositoryFactory>();
        _tokenRepo = Substitute.For<ITokenRepository>();
        _mediaRepo = Substitute.For<IMediaRepository>();
        _ratingRepo = Substitute.For<IRatingRepository>();

        _factory.CreateTokenRepository().Returns(_tokenRepo);
        _factory.CreateMediaRepository().Returns(_mediaRepo);
        _factory.CreateRatingRepository().Returns(_ratingRepo);
    }

    [Test]
    public void NoAuth_DoesNothing() {
        IAuth auth = new NoAuth();

        Assert.DoesNotThrow(() => auth.Auth(new Token { Id = "any" }, 0, _factory));
        Assert.That(auth.UserId, Is.Null);
    }

    [Test]
    public void SimpleAuth_ValidToken_SetsUserId() {
        var token = new Token { Id = "valid" };
        var verified = new Token { Id = "valid", UserId = 1, ValidUntil = DateTime.Now.AddHours(1) };
        _tokenRepo.GetToken(token).Returns(verified);

        IAuth auth = new SimpleAuth();
        auth.Auth(token, 0, _factory);

        Assert.That(auth.UserId, Is.EqualTo(1));
    }

    [Test]
    public void SimpleAuth_InvalidToken_Throws() {
        var token = new Token { Id = "invalid" };
        _tokenRepo.GetToken(token).Returns((Token?)null);

        IAuth auth = new SimpleAuth();

        Assert.Throws<ApiBadLoginDataException>(() => auth.Auth(token, 0, _factory));
    }

    [Test]
    public void SimpleAuth_ExpiredToken_DeletesAndThrows() {
        var token = new Token { Id = "expired" };
        var verified = new Token { Id = "expired", UserId = 7, ValidUntil = DateTime.Now.AddMinutes(-1) };
        _tokenRepo.GetToken(token).Returns(verified);

        IAuth auth = new SimpleAuth();

        Assert.Throws<ApiTokenExpiredException>(() => auth.Auth(token, 0, _factory));
        _tokenRepo.Received(1).DeleteToken(verified);
    }

    [Test]
    public void UserAuth_ValidComparator_SetsUserId() {
        var token = new Token { Id = "valid" };
        var verified = new Token { Id = "valid", UserId = 2, ValidUntil = DateTime.Now.AddHours(1) };
        _tokenRepo.GetToken(token).Returns(verified);

        IAuth auth = new UserAuth();
        auth.Auth(token, 2, _factory);

        Assert.That(auth.UserId, Is.EqualTo(2));
    }

    [Test]
    public void UserAuth_InvalidComparator_ThrowsNoAccess() {
        var token = new Token { Id = "valid" };
        var verified = new Token { Id = "valid", UserId = 2, ValidUntil = DateTime.Now.AddHours(1) };
        _tokenRepo.GetToken(token).Returns(verified);

        IAuth auth = new UserAuth();

        Assert.Throws<ApiNoAccessException>(() => auth.Auth(token, 99, _factory));
    }

    [Test]
    public void MediaAuth_ValidOwner_SetsUserId() {
        var token = new Token { Id = "valid" };
        var verified = new Token { Id = "valid", UserId = 5, ValidUntil = DateTime.Now.AddHours(1) };
        _tokenRepo.GetToken(token).Returns(verified);

        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 10))
            .Returns(new Media { Id = 10, Owner = 5 });

        IAuth auth = new MediaAuth();
        auth.Auth(token, 10, _factory);

        Assert.That(auth.UserId, Is.EqualTo(5));
    }

    [Test]
    public void MediaAuth_MediaMissing_Throws() {
        var token = new Token { Id = "valid" };
        var verified = new Token { Id = "valid", UserId = 5, ValidUntil = DateTime.Now.AddHours(1) };
        _tokenRepo.GetToken(token).Returns(verified);

        _mediaRepo.GetMedia(Arg.Any<Media>()).Returns((Media?)null);

        IAuth auth = new MediaAuth();

        Assert.Throws<ApiItemDoesNotExistException>(() => auth.Auth(token, 10, _factory));
    }

    [Test]
    public void MediaAuth_OwnerMismatch_ThrowsNoAccess() {
        var token = new Token { Id = "valid" };
        var verified = new Token { Id = "valid", UserId = 5, ValidUntil = DateTime.Now.AddHours(1) };
        _tokenRepo.GetToken(token).Returns(verified);

        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 10))
            .Returns(new Media { Id = 10, Owner = 99 });

        IAuth auth = new MediaAuth();

        Assert.Throws<ApiNoAccessException>(() => auth.Auth(token, 10, _factory));
    }

    [Test]
    public void RatingAuth_ValidOwner_SetsUserId() {
        var token = new Token { Id = "valid" };
        var verified = new Token { Id = "valid", UserId = 3, ValidUntil = DateTime.Now.AddHours(1) };
        _tokenRepo.GetToken(token).Returns(verified);

        _ratingRepo.GetRatingById(Arg.Is<Rating>(r => r.Id == 20))
            .Returns(new Rating { Id = 20, Owner = 3 });

        IAuth auth = new RatingAuth();
        auth.Auth(token, 20, _factory);

        Assert.That(auth.UserId, Is.EqualTo(3));
    }

    [Test]
    public void RatingAuth_RatingMissing_Throws() {
        var token = new Token { Id = "valid" };
        var verified = new Token { Id = "valid", UserId = 3, ValidUntil = DateTime.Now.AddHours(1) };
        _tokenRepo.GetToken(token).Returns(verified);

        _ratingRepo.GetRatingById(Arg.Any<Rating>()).Returns((Rating?)null);

        IAuth auth = new RatingAuth();

        Assert.Throws<ApiItemDoesNotExistException>(() => auth.Auth(token, 20, _factory));
    }

    [Test]
    public void RatingAuth_OwnerMismatch_ThrowsNoAccess() {
        var token = new Token { Id = "valid" };
        var verified = new Token { Id = "valid", UserId = 3, ValidUntil = DateTime.Now.AddHours(1) };
        _tokenRepo.GetToken(token).Returns(verified);

        _ratingRepo.GetRatingById(Arg.Is<Rating>(r => r.Id == 20))
            .Returns(new Rating { Id = 20, Owner = 99 });

        IAuth auth = new RatingAuth();

        Assert.Throws<ApiNoAccessException>(() => auth.Auth(token, 20, _factory));
    }
}