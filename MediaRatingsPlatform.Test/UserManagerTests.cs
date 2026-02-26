using MediaRatingsPlatform.BusinessLayer;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;
using NSubstitute;

namespace MediaRatingsPlatform.Test;

[TestFixture]
public class UserManagerTests {
    [SetUp]
    public void SetUp() {
        _factory = Substitute.For<IRepositoryFactory>();
        _userRepo = Substitute.For<IUserRepository>();
        _mediaRepo = Substitute.For<IMediaRepository>();
        _ratingRepo = Substitute.For<IRatingRepository>();

        _factory.CreateUserRepository().Returns(_userRepo);
        _factory.CreateMediaRepository().Returns(_mediaRepo);
        _factory.CreateRatingRepository().Returns(_ratingRepo);

        _sut = new UserManager(_factory);
    }

    private IRepositoryFactory _factory = null!;
    private IUserRepository _userRepo = null!;
    private IMediaRepository _mediaRepo = null!;
    private IRatingRepository _ratingRepo = null!;
    private UserManager _sut = null!;

    [Test]
    public void GetProfile_PopulatesStatistics() {
        var input = new User { Id = 1 };
        var user = new User { Id = 1, Username = "u" };

        _userRepo.GetUser(input).Returns(user);
        _mediaRepo.CountMediaByUser(user).Returns(4);

        _ratingRepo.GetRatingsByUser(user).Returns(new List<Rating> {
            new() { Stars = 5 },
            new() { Stars = 3 },
            new() { Stars = null }
        });

        var result = _sut.GetProfile(input);

        Assert.That(result, Is.SameAs(user));
        Assert.That(result.TotalMediaStatistic, Is.EqualTo(4));
        Assert.That(result.TotalRatingsStatistic, Is.EqualTo(2));
        Assert.That(result.AverageRatingStatistic, Is.EqualTo(4)); // (5+3)/2
    }

    [Test]
    public void GetProfile_WhenNoRatings_SetsZeroAverageAndCount() {
        var input = new User { Id = 2 };
        var user = new User { Id = 2, Username = "u2" };

        _userRepo.GetUser(input).Returns(user);
        _mediaRepo.CountMediaByUser(user).Returns(0);
        _ratingRepo.GetRatingsByUser(user).Returns(new List<Rating>());

        var result = _sut.GetProfile(input);

        Assert.That(result.TotalRatingsStatistic, Is.EqualTo(0));
        Assert.That(result.AverageRatingStatistic, Is.EqualTo(0));
    }

    [Test]
    public void UpdateProfile_DelegatesToRepository() {
        var input = new User { Id = 3, FavoriteGenre = "Sci-Fi" };

        _sut.UpdateProfile(input);

        _userRepo.Received(1).SetUserPreferences(input);
    }
}
