using MediaRatingsPlatform.BusinessLayer;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;
using NSubstitute;

namespace MediaRatingsPlatform.Test;

[TestFixture]
public class StatisticsManagerTests {
    [SetUp]
    public void SetUp() {
        _factory = Substitute.For<IRepositoryFactory>();
        _ratingRepo = Substitute.For<IRatingRepository>();
        _userRepo = Substitute.For<IUserRepository>();

        _factory.CreateRatingRepository().Returns(_ratingRepo);
        _factory.CreateUserRepository().Returns(_userRepo);

        _sut = new StatisticsManager(_factory);
    }

    private IRepositoryFactory _factory = null!;
    private IRatingRepository _ratingRepo = null!;
    private IUserRepository _userRepo = null!;
    private StatisticsManager _sut = null!;

    [Test]
    public void GetLeaderboard_MapsUserIdsToUsernames() {
        var idLeaderboard = new List<LeaderboardEntry> {
            new() { User = 1, Ratings = 5 },
            new() { User = 2, Ratings = 3 }
        };

        _ratingRepo.GetLeaderboard().Returns(idLeaderboard);
        _userRepo.GetUser(Arg.Is<User>(u => u.Id == 1)).Returns(new User { Id = 1, Username = "alice" });
        _userRepo.GetUser(Arg.Is<User>(u => u.Id == 2)).Returns(new User { Id = 2, Username = "bob" });

        var result = _sut.GetLeaderboard();

        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result[0].User, Is.EqualTo("alice"));
        Assert.That(result[0].Ratings, Is.EqualTo(5));
        Assert.That(result[1].User, Is.EqualTo("bob"));
        Assert.That(result[1].Ratings, Is.EqualTo(3));
    }

    [Test]
    public void GetLeaderboard_PreservesOrderingFromRepository() {
        var idLeaderboard = new List<LeaderboardEntry> {
            new() { User = 10, Ratings = 9 },
            new() { User = 5, Ratings = 12 }
        };

        _ratingRepo.GetLeaderboard().Returns(idLeaderboard);
        _userRepo.GetUser(Arg.Is<User>(u => u.Id == 10)).Returns(new User { Id = 10, Username = "first" });
        _userRepo.GetUser(Arg.Is<User>(u => u.Id == 5)).Returns(new User { Id = 5, Username = "second" });

        var result = _sut.GetLeaderboard();

        Assert.That(result[0].User, Is.EqualTo("first"));
        Assert.That(result[1].User, Is.EqualTo("second"));
    }

    [Test]
    public void GetLeaderboard_WhenEmpty_ReturnsEmpty() {
        _ratingRepo.GetLeaderboard().Returns(new List<LeaderboardEntry>());

        var result = _sut.GetLeaderboard();

        Assert.That(result, Is.Empty);
        _userRepo.DidNotReceive().GetUser(Arg.Any<User>());
    }
}
