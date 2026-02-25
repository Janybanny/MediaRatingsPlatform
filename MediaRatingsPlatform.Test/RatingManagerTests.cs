using MediaRatingsPlatform.BusinessLayer;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;
using NSubstitute;

namespace MediaRatingsPlatform.Test;

[TestFixture]
public class RatingManagerTests {
    [SetUp]
    public void SetUp() {
        _factory = Substitute.For<IRepositoryFactory>();
        _mediaRepo = Substitute.For<IMediaRepository>();
        _ratingRepo = Substitute.For<IRatingRepository>();
        _likeRepo = Substitute.For<ILikeRepository>();

        _factory.CreateMediaRepository().Returns(_mediaRepo);
        _factory.CreateRatingRepository().Returns(_ratingRepo);
        _factory.CreateLikeRepository().Returns(_likeRepo);

        _sut = new RatingManager(_factory);
    }

    private IRepositoryFactory _factory = null!;
    private IMediaRepository _mediaRepo = null!;
    private IRatingRepository _ratingRepo = null!;
    private ILikeRepository _likeRepo = null!;
    private RatingManager _sut = null!;

    [Test]
    public void RateMedia_WhenMediaMissing_Throws() {
        var rating = new Rating { MediaId = 1, Owner = 2, Stars = 4, Comment = "ok" };
        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 1)).Returns((Media?)null);

        Assert.Throws<ApiItemDoesNotExistException>(() => _sut.RateMedia(rating));
        _ratingRepo.DidNotReceive().AddRating(Arg.Any<Rating>());
    }

    [Test]
    public void RateMedia_WhenNoExistingRating_AddsNewRating() {
        var rating = new Rating { MediaId = 1, Owner = 2, Stars = 4, Comment = null };
        _mediaRepo.GetMedia(Arg.Any<Media>()).Returns(new Media { Id = 1 });
        _ratingRepo.GetRatingByMediaAndUser(Arg.Any<Rating>()).Returns((Rating?)null);

        _sut.RateMedia(rating);

        Assert.That(rating.Comment, Is.EqualTo(string.Empty));
        Assert.That(rating.CommentVisible, Is.False);
        Assert.That(rating.CreatedAt, Is.Not.Null);
        _ratingRepo.Received(1).AddRating(rating);
    }

    [Test]
    public void RateMedia_WhenExistingRating_UpdatesExisting() {
        var rating = new Rating { MediaId = 1, Owner = 2, Stars = 3, Comment = "new" };
        var existing = new Rating { Id = 99, MediaId = 1, Owner = 2 };

        _mediaRepo.GetMedia(Arg.Any<Media>()).Returns(new Media { Id = 1 });
        _ratingRepo.GetRatingByMediaAndUser(Arg.Any<Rating>()).Returns(existing);

        _sut.RateMedia(rating);

        Assert.That(rating.Id, Is.EqualTo(99));
        _ratingRepo.Received(1).UpdateRating(rating);
    }

    [Test]
    public void ComfirmRatingComment_WhenRatingMissing_Throws() {
        var rating = new Rating { Id = 5 };
        _ratingRepo.GetRatingById(rating).Returns((Rating?)null);

        Assert.Throws<ApiItemDoesNotExistException>(() => _sut.ComfirmRatingComment(rating));
    }

    [Test]
    public void ComfirmRatingComment_SetsVisibleAndUpdates() {
        var rating = new Rating { Id = 5 };
        var existing = new Rating { Id = 5, CommentVisible = false };

        _ratingRepo.GetRatingById(rating).Returns(existing);

        _sut.ComfirmRatingComment(rating);

        Assert.That(existing.CommentVisible, Is.True);
        _ratingRepo.Received(1).UpdateRating(existing);
    }

    [Test]
    public void LikeRating_WhenRatingMissing_Throws() {
        var like = new Like { UserId = 1, RatingId = 10 };
        _ratingRepo.GetRatingById(Arg.Is<Rating>(r => r.Id == 10)).Returns((Rating?)null);

        Assert.Throws<ApiItemDoesNotExistException>(() => _sut.LikeRating(like));
        _likeRepo.DidNotReceive().LikeRating(Arg.Any<Like>());
    }

    [Test]
    public void LikeRating_WhenNotLikedYet_AddsLike() {
        var like = new Like { UserId = 1, RatingId = 10 };
        _ratingRepo.GetRatingById(Arg.Any<Rating>()).Returns(new Rating { Id = 10 });
        _likeRepo.GetLike(like).Returns(false);

        _sut.LikeRating(like);

        _likeRepo.Received(1).LikeRating(like);
    }

    [Test]
    public void LikeRating_WhenAlreadyLiked_DoesNothing() {
        var like = new Like { UserId = 1, RatingId = 10 };
        _ratingRepo.GetRatingById(Arg.Any<Rating>()).Returns(new Rating { Id = 10 });
        _likeRepo.GetLike(like).Returns(true);

        _sut.LikeRating(like);

        _likeRepo.DidNotReceive().LikeRating(Arg.Any<Like>());
    }

    [Test]
    public void UpdateRating_WhenRatingMissing_Throws() {
        var rating = new Rating { Id = 7, Stars = 5 };
        _ratingRepo.GetRatingById(rating).Returns((Rating?)null);

        Assert.Throws<ApiItemDoesNotExistException>(() => _sut.UpdateRating(rating));
    }

    [Test]
    public void UpdateRating_UsesExistingMediaAndOwner_AndUpdates() {
        var rating = new Rating { Id = 7, Stars = 5, Comment = null };
        var existing = new Rating { Id = 7, MediaId = 3, Owner = 9 };

        _ratingRepo.GetRatingById(rating).Returns(existing);

        _sut.UpdateRating(rating);

        Assert.That(rating.MediaId, Is.EqualTo(3));
        Assert.That(rating.Owner, Is.EqualTo(9));
        Assert.That(rating.Comment, Is.EqualTo(string.Empty));
        Assert.That(rating.CommentVisible, Is.False);
        Assert.That(rating.CreatedAt, Is.Not.Null);
        _ratingRepo.Received(1).UpdateRating(rating);
    }

    [Test]
    public void DeleteRating_CallsRepository() {
        var rating = new Rating { Id = 12 };

        _sut.DeleteRating(rating);

        _ratingRepo.Received(1).DeleteRating(rating);
    }

    [Test]
    public void GetUserRatings_ReturnsRepositoryResults() {
        var user = new User { Id = 4 };
        var ratings = new List<Rating> { new() { Id = 1 }, new() { Id = 2 } };
        _ratingRepo.GetRatingsByUser(user).Returns(ratings);

        var result = _sut.GetUserRatings(user);

        Assert.That(result, Is.SameAs(ratings));
    }
}
