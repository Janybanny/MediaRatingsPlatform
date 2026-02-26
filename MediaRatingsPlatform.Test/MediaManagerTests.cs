using MediaRatingsPlatform.BusinessLayer;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;
using NSubstitute;

namespace MediaRatingsPlatform.Test;

[TestFixture]
public class MediaManagerTests {
    [SetUp]
    public void SetUp() {
        _factory = Substitute.For<IRepositoryFactory>();
        _mediaRepo = Substitute.For<IMediaRepository>();
        _genreRepo = Substitute.For<IGenreRepository>();
        _ratingRepo = Substitute.For<IRatingRepository>();

        _factory.CreateMediaRepository().Returns(_mediaRepo);
        _factory.CreateGenreRepository().Returns(_genreRepo);
        _factory.CreateRatingRepository().Returns(_ratingRepo);

        _sut = new MediaManager(_factory);
    }

    private IRepositoryFactory _factory = null!;
    private IMediaRepository _mediaRepo = null!;
    private IGenreRepository _genreRepo = null!;
    private IRatingRepository _ratingRepo = null!;
    private MediaManager _sut = null!;

    [Test]
    public void GetMedia_WhenMediaMissing_Throws() {
        _mediaRepo.GetMedia(Arg.Any<Media>()).Returns((Media?)null);

        Assert.Throws<ApiItemDoesNotExistException>(() => _sut.GetMedia(new Media { Id = 1 }, 1));
    }

    [Test]
    public void GetMedia_PopulatesGenresAndRatings_AndMasksHiddenComments() {
        var media = new Media { Id = 10, Title = "X" };
        var genres = new List<Genre> {
            new() { MediaId = 10, Name = "Action" },
            new() { MediaId = 10, Name = "Drama" }
        };
        var ratings = new List<Rating> {
            new() { Owner = 1, Stars = 4, CommentVisible = true, Comment = "ok" },
            new() { Owner = 2, Stars = 2, CommentVisible = false, Comment = "hidden" }
        };

        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 10)).Returns(media);
        _genreRepo.GetGenres(Arg.Is<Genre>(g => g.MediaId == 10)).Returns(genres);
        _ratingRepo.GetRatingsByMedia(Arg.Is<Media>(m => m.Id == 10)).Returns(ratings);

        var result = _sut.GetMedia(new Media { Id = 10 }, 1);

        Assert.That(result.Genres, Is.EquivalentTo(["Action", "Drama"]));
        Assert.That(result.Ratings, Has.Count.EqualTo(2));
        Assert.That(result.Ratings[1].Comment, Is.Null);
        Assert.That(result.AverageRating, Is.EqualTo(3f));
    }

    [Test]
    public void AddMedia_AddsGenresWithNewId() {
        var media = new Media { Title = "Y", Genres = ["Sci-Fi", "Adventure"] };
        _mediaRepo.AddMedia(media).Returns(123);

        _sut.AddMedia(media);

        Assert.That(media.Id, Is.EqualTo(123));
        _genreRepo.Received(1).AddGenre(Arg.Is<Genre>(g => g.MediaId == 123 && g.Name == "Sci-Fi"));
        _genreRepo.Received(1).AddGenre(Arg.Is<Genre>(g => g.MediaId == 123 && g.Name == "Adventure"));
    }

    [Test]
    public void UpdateMedia_RemovesMissingGenres_AndAddsNewOnes() {
        var media = new Media { Id = 55, Genres = ["Action", "Comedy"] };
        var existing = new List<Genre> {
            new() { MediaId = 55, Name = "Action" },
            new() { MediaId = 55, Name = "Drama" }
        };

        _genreRepo.GetGenres(Arg.Is<Genre>(g => g.MediaId == 55)).Returns(existing);

        _sut.UpdateMedia(media);

        _mediaRepo.Received(1).UpdateMedia(media);
        _genreRepo.Received(1).RemoveGenre(Arg.Is<Genre>(g => g.MediaId == 55 && g.Name == "Drama"));
        _genreRepo.Received(1).AddGenre(Arg.Is<Genre>(g => g.MediaId == 55 && g.Name == "Comedy"));
    }

    [Test]
    public void DeleteMedia_CallsRepository() {
        var media = new Media { Id = 77 };

        _sut.DeleteMedia(media);

        _mediaRepo.Received(1).DeleteMedia(media);
    }

    [Test]
    public void ListMedias_FiltersAndSorts() {
        _mediaRepo.GetAllMediaIds().Returns([1, 2, 3]);

        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 1))
            .Returns(new Media { Id = 1, Title = "Alpha", ReleaseYear = 2020, MediaType = "Movie" });
        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 2))
            .Returns(new Media { Id = 2, Title = "Beta", ReleaseYear = 2019, MediaType = "Movie" });
        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 3))
            .Returns(new Media { Id = 3, Title = "Gamma", ReleaseYear = 2021, MediaType = "Show" });

        _genreRepo.GetGenres(Arg.Any<Genre>()).Returns([]);
        _ratingRepo.GetRatingsByMedia(Arg.Any<Media>()).Returns([]);

        var filter = new MediaFilter {
            MediaType = "Movie",
            SortBy = "year"
        };

        var result = _sut.ListMedias(filter, 1).ToList();

        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result[0].Id, Is.EqualTo(2));
        Assert.That(result[1].Id, Is.EqualTo(1));
    }

    [Test]
    public void ListMedias_FiltersByTitleGenreRatingAndAgeRestriction() {
        _mediaRepo.GetAllMediaIds().Returns([1, 2, 3]);

        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 1))
            .Returns(new Media { Id = 1, Title = "Star Quest", MediaType = "Movie", ReleaseYear = 2020, AgeRestriction = 16 });
        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 2))
            .Returns(new Media { Id = 2, Title = "Star Kids", MediaType = "Movie", ReleaseYear = 2021, AgeRestriction = 12 });
        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 3))
            .Returns(new Media { Id = 3, Title = "Moon Tale", MediaType = "Movie", ReleaseYear = 2022, AgeRestriction = 18 });

        _genreRepo.GetGenres(Arg.Is<Genre>(g => g.MediaId == 1))
            .Returns([new Genre { MediaId = 1, Name = "Sci-Fi" }]);
        _genreRepo.GetGenres(Arg.Is<Genre>(g => g.MediaId == 2))
            .Returns([new Genre { MediaId = 2, Name = "Sci-Fi" }]);
        _genreRepo.GetGenres(Arg.Is<Genre>(g => g.MediaId == 3))
            .Returns([new Genre { MediaId = 3, Name = "Drama" }]);

        _ratingRepo.GetRatingsByMedia(Arg.Is<Media>(m => m.Id == 1))
            .Returns([new Rating { Stars = 5, CommentVisible = true, Owner = 1 }]);
        _ratingRepo.GetRatingsByMedia(Arg.Is<Media>(m => m.Id == 2))
            .Returns([new Rating { Stars = 2, CommentVisible = true, Owner = 1 }]);
        _ratingRepo.GetRatingsByMedia(Arg.Is<Media>(m => m.Id == 3))
            .Returns([new Rating { Stars = 4, CommentVisible = true, Owner = 1 }]);

        var filter = new MediaFilter {
            Title = "star",
            Genre = "sci-fi",
            Rating = 4,
            AgeRestriction = 16,
            SortBy = "title"
        };

        var result = _sut.ListMedias(filter, 1).ToList();

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Id, Is.EqualTo(1));
    }

    [Test]
    public void ListMedias_SortsByScoreDescending() {
        _mediaRepo.GetAllMediaIds().Returns([1, 2]);

        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 1))
            .Returns(new Media { Id = 1, Title = "A" });
        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 2))
            .Returns(new Media { Id = 2, Title = "B" });

        _genreRepo.GetGenres(Arg.Any<Genre>()).Returns([]);

        _ratingRepo.GetRatingsByMedia(Arg.Is<Media>(m => m.Id == 1))
            .Returns([new Rating { Stars = 2, CommentVisible = true, Owner = 1 }]);
        _ratingRepo.GetRatingsByMedia(Arg.Is<Media>(m => m.Id == 2))
            .Returns([new Rating { Stars = 5, CommentVisible = true, Owner = 1 }]);

        var filter = new MediaFilter { SortBy = "score" };

        var result = _sut.ListMedias(filter, 1).ToList();

        Assert.That(result[0].Id, Is.EqualTo(2));
        Assert.That(result[1].Id, Is.EqualTo(1));
    }

    [Test]
    public void ListMedias_UsesGetMediaForEachId_AndReturnsPopulatedMedia() {
        _mediaRepo.GetAllMediaIds().Returns([10, 11]);

        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 10))
            .Returns(new Media { Id = 10, Title = "X" });
        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 11))
            .Returns(new Media { Id = 11, Title = "Y" });

        _genreRepo.GetGenres(Arg.Is<Genre>(g => g.MediaId == 10))
            .Returns([new Genre { MediaId = 10, Name = "Action" }]);
        _genreRepo.GetGenres(Arg.Is<Genre>(g => g.MediaId == 11))
            .Returns([new Genre { MediaId = 11, Name = "Drama" }]);

        _ratingRepo.GetRatingsByMedia(Arg.Is<Media>(m => m.Id == 10))
            .Returns([new Rating { Stars = 4, CommentVisible = true, Owner = 1 }]);
        _ratingRepo.GetRatingsByMedia(Arg.Is<Media>(m => m.Id == 11))
            .Returns([]);

        var result = _sut.ListMedias(new MediaFilter(), 1).ToList();

        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result.Single(m => m.Id == 10).Genres, Is.EquivalentTo(["Action"]));
        Assert.That(result.Single(m => m.Id == 11).Genres, Is.EquivalentTo(["Drama"]));
    }

    [Test]
    public void ListMedias_WhenAnyMediaMissing_Throws() {
        _mediaRepo.GetAllMediaIds().Returns([1, 2]);

        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 1))
            .Returns(new Media { Id = 1, Title = "Ok" });
        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 2))
            .Returns((Media?)null);

        _genreRepo.GetGenres(Arg.Any<Genre>()).Returns([]);
        _ratingRepo.GetRatingsByMedia(Arg.Any<Media>()).Returns([]);

        Assert.Throws<ApiItemDoesNotExistException>(() => _sut.ListMedias(new MediaFilter(), 1));
    }
}
