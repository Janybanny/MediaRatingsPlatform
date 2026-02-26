using MediaRatingsPlatform.BusinessLayer;
using MediaRatingsPlatform.BusinessLayer.Interfaces;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;
using NSubstitute;

namespace MediaRatingsPlatform.Test;

[TestFixture]
public class RecommendationManagerTests {
    [SetUp]
    public void SetUp() {
        _factory = Substitute.For<IRepositoryFactory>();
        _ratingRepo = Substitute.For<IRatingRepository>();
        _genreRepo = Substitute.For<IGenreRepository>();
        _mediaRepo = Substitute.For<IMediaRepository>();
        _mediaManager = Substitute.For<IMediaManager>();

        _factory.CreateRatingRepository().Returns(_ratingRepo);
        _factory.CreateGenreRepository().Returns(_genreRepo);
        _factory.CreateMediaRepository().Returns(_mediaRepo);

        _sut = new RecommendationManager(_factory);
    }

    private IRepositoryFactory _factory = null!;
    private IRatingRepository _ratingRepo = null!;
    private IGenreRepository _genreRepo = null!;
    private IMediaRepository _mediaRepo = null!;
    private IMediaManager _mediaManager = null!;
    private RecommendationManager _sut = null!;

    [Test]
    public void GetRecommendationsByGenre_OrdersByGenreWeights() {
        var user = new User { Id = 1 };
        var ratings = new List<Rating> {
            new() { MediaId = 10, Stars = 5 },
            new() { MediaId = 20, Stars = 2 }
        };

        _ratingRepo.GetRatingsByUser(user).Returns(ratings);

        _genreRepo.GetGenres(Arg.Is<Genre>(g => g.MediaId == 10))
            .Returns([new Genre { MediaId = 10, Name = "Action" }]);
        _genreRepo.GetGenres(Arg.Is<Genre>(g => g.MediaId == 20))
            .Returns([new Genre { MediaId = 20, Name = "Drama" }]);

        _genreRepo.GetAllGenreEntries().Returns([
            new Genre { MediaId = 10, Name = "Action" },
            new Genre { MediaId = 20, Name = "Drama" }
        ]);

        _mediaManager.GetMedia(Arg.Is<Media>(m => m.Id == 10), 1).Returns(new Media { Id = 10, Title = "A" });
        _mediaManager.GetMedia(Arg.Is<Media>(m => m.Id == 20), 1).Returns(new Media { Id = 20, Title = "B" });

        var result = _sut.GetRecommendationsByGenre(user, _mediaManager);

        Assert.That(result.Select(m => m.Id), Is.EqualTo([10, 20]));
    }

    [Test]
    public void GetRecommendationsByContent_MergesGenreTypeAgeWeights() {
        var user = new User { Id = 1 };
        var ratings = new List<Rating> {
            new() { MediaId = 10, Stars = 4 },
            new() { MediaId = 20, Stars = 5 }
        };

        _ratingRepo.GetRatingsByUser(user).Returns(ratings);

        _genreRepo.GetGenres(Arg.Is<Genre>(g => g.MediaId == 10))
            .Returns([new Genre { MediaId = 10, Name = "Action" }]);
        _genreRepo.GetGenres(Arg.Is<Genre>(g => g.MediaId == 20))
            .Returns([new Genre { MediaId = 20, Name = "Action" }]);

        _genreRepo.GetAllGenreEntries().Returns([
            new Genre { MediaId = 10, Name = "Action" },
            new Genre { MediaId = 20, Name = "Action" }
        ]);

        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 10))
            .Returns(new Media { Id = 10, MediaType = "Movie", AgeRestriction = 12 });
        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 20))
            .Returns(new Media { Id = 20, MediaType = "Movie", AgeRestriction = 12 });

        _mediaManager.GetMedia(Arg.Is<Media>(m => m.Id == 10), 1).Returns(new Media { Id = 10, Title = "A" });
        _mediaManager.GetMedia(Arg.Is<Media>(m => m.Id == 20), 1).Returns(new Media { Id = 20, Title = "B" });

        var result = _sut.GetRecommendationsByContent(user, _mediaManager);

        Assert.That(result.Select(m => m.Id).ToList(), Is.EqualTo([10, 20]));
    }

    [Test]
    public void GetRecommendationsByGenre_WhenNoRatings_ReturnsEmpty() {
        var user = new User { Id = 1 };
        _ratingRepo.GetRatingsByUser(user).Returns([]);

        var result = _sut.GetRecommendationsByGenre(user, _mediaManager);

        Assert.That(result, Is.Empty);
        _mediaManager.DidNotReceive().GetMedia(Arg.Any<Media>(), Arg.Any<int>());
    }

    [Test]
    public void GetRecommendationsByContent_WhenNoRatings_ReturnsEmpty() {
        var user = new User { Id = 1 };
        _ratingRepo.GetRatingsByUser(user).Returns([]);

        var result = _sut.GetRecommendationsByContent(user, _mediaManager);

        Assert.That(result, Is.Empty);
        _mediaManager.DidNotReceive().GetMedia(Arg.Any<Media>(), Arg.Any<int>());
    }

    [Test]
    public void GetRecommendationsByGenre_CallsMediaManagerForEachWeightedMediaId() {
        var user = new User { Id = 1 };
        var ratings = new List<Rating> { new() { MediaId = 10, Stars = 5 } };

        _ratingRepo.GetRatingsByUser(user).Returns(ratings);

        _genreRepo.GetGenres(Arg.Is<Genre>(g => g.MediaId == 10))
            .Returns([new Genre { MediaId = 10, Name = "Action" }]);

        _genreRepo.GetAllGenreEntries().Returns([
            new Genre { MediaId = 10, Name = "Action" },
            new Genre { MediaId = 20, Name = "Action" }
        ]);

        _mediaManager.GetMedia(Arg.Any<Media>(), 1).Returns(new Media());

        _sut.GetRecommendationsByGenre(user, _mediaManager);

        _mediaManager.Received(1).GetMedia(Arg.Is<Media>(m => m.Id == 10), 1);
        _mediaManager.Received(1).GetMedia(Arg.Is<Media>(m => m.Id == 20), 1);
    }
}
