using MediaRatingsPlatform.BusinessLayer;
using MediaRatingsPlatform.DataAccessLayer.Interfaces;
using MediaRatingsPlatform.SharedObjects;
using NSubstitute;

namespace MediaRatingsPlatform.Test;

[TestFixture]
public class FavouriteManagerTests {
    [SetUp]
    public void SetUp() {
        _factory = Substitute.For<IRepositoryFactory>();
        _mediaRepo = Substitute.For<IMediaRepository>();
        _favouriteRepo = Substitute.For<IFavouriteRepository>();

        _factory.CreateMediaRepository().Returns(_mediaRepo);
        _factory.CreateFavouriteRepository().Returns(_favouriteRepo);

        _sut = new FavouriteManager(_factory);
    }

    private IRepositoryFactory _factory = null!;
    private IMediaRepository _mediaRepo = null!;
    private IFavouriteRepository _favouriteRepo = null!;
    private FavouriteManager _sut = null!;

    [Test]
    public void Favourite_WhenMediaDoesNotExist_Throws() {
        var fav = new Favourite { UserId = 1, MediaId = 42 };
        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 42)).Returns((Media?)null);

        Assert.Throws<ApiItemDoesNotExistException>(() => _sut.Favourite(fav));
        _favouriteRepo.DidNotReceive().Favourite(Arg.Any<Favourite>());
    }

    [Test]
    public void Favourite_WhenMediaExists_CallsRepository() {
        var fav = new Favourite { UserId = 1, MediaId = 42 };
        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 42)).Returns(new Media { Id = 42 });

        _sut.Favourite(fav);

        _favouriteRepo.Received(1).Favourite(fav);
    }

    [Test]
    public void UnFavourite_WhenMediaDoesNotExist_Throws() {
        var fav = new Favourite { UserId = 1, MediaId = 99 };
        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 99)).Returns((Media?)null);

        Assert.Throws<ApiItemDoesNotExistException>(() => _sut.UnFavourite(fav));
        _favouriteRepo.DidNotReceive().Unfavourite(Arg.Any<Favourite>());
    }

    [Test]
    public void UnFavourite_WhenMediaExists_CallsRepository() {
        var fav = new Favourite { UserId = 1, MediaId = 99 };
        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 99)).Returns(new Media { Id = 99 });

        _sut.UnFavourite(fav);

        _favouriteRepo.Received(1).Unfavourite(fav);
    }

    [Test]
    public void GetFavourites_ReturnsResolvedMediaList() {
        var user = new User { Id = 7 };
        var favourites = new List<Favourite> {
            new() { UserId = 7, MediaId = 1 },
            new() { UserId = 7, MediaId = 2 }
        };

        _favouriteRepo.GetFavourites(user).Returns(favourites);
        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 1)).Returns(new Media { Id = 1, Title = "A" });
        _mediaRepo.GetMedia(Arg.Is<Media>(m => m.Id == 2)).Returns(new Media { Id = 2, Title = "B" });

        var result = _sut.GetFavourites(user);

        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result[0].Id, Is.EqualTo(1));
        Assert.That(result[1].Id, Is.EqualTo(2));
    }
}
