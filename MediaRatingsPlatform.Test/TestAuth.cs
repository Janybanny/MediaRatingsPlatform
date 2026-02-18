namespace MediaRatingsPlatform.Test;

internal class TestAuth {
    // TODO Test Auth
    /*
    [Test]
    public void TestLogin() {
        Assert.Pass();
    }

    [Test]
    public void TestNoAuth() {
        IAuth auth = new NoAuth();
        auth.Auth(new Token { Id = "nothing should happen" }, 0);
        Assert.Pass();
    }

    [Test]
    public void TestSimpleAuthValidToken() {
        // arrange
        var db = Substitute.For<ITokenRepository>();
        db.GetToken(new Token { Id = "valid" }).Returns(new Token { Id = "valid", UserId = 1, ValidUntil = DateTime.Now.AddHours(1) });
        IAuth auth = new SimpleAuth();
        // act
        auth.Auth(new Token { Id = "valid" }, 0);
        // assert
        Assert.That(auth.UserId, Is.EqualTo(1));
    }

    [Test]
    public void TestSimpleAuthInvalidToken() {
        // arrange
        var db = Substitute.For<ITokenRepository>();
        db.GetToken(new Token { Id = "invalid" }).ReturnsNull();
        IAuth auth = new SimpleAuth();
        // act & assert
        Assert.Throws<ApiBadLoginDataException>(() => { auth.Auth(new Token { Id = "invalid" }, 0); });
    }

    [Test]
    public void TestUserAuthValid() {
        // arrange
        var db = Substitute.For<ITokenRepository>();
        db.GetToken(new Token { Id = "valid" }).Returns(new Token { Id = "valid", UserId = 1, ValidUntil = DateTime.Now.AddHours(1) });
        IAuth auth = new SimpleAuth();
        // act
        auth.Auth(new Token { Id = "valid" }, 1);
        // assert
        Assert.That(auth.UserId, Is.EqualTo(1));
    }

    [Test]
    public void TestUserAuthInvalidToken() {
        // arrange
        var db = Substitute.For<ITokenRepository>();
        db.GetToken(new Token { Id = "invalid" }).ReturnsNull();
        IAuth auth = new SimpleAuth();
        // act & assert
        Assert.Throws<ApiBadLoginDataException>(() => { auth.Auth(new Token { Id = "invalid" }, 1); });
    }

    [Test]
    public void TestUserAuthInvalidComparator() {
        // arrange
        var db = Substitute.For<ITokenRepository>();
        db.GetToken(new Token { Id = "valid" }).Returns(new Token { Id = "valid", UserId = 1, ValidUntil = DateTime.Now.AddHours(1) });
        IAuth auth = new SimpleAuth();
        // act & assert
        Assert.Throws<ApiNoAccessException>(() => { auth.Auth(new Token { Id = "valid" }, 0); });
    }

    [Test]
    public void TestUserAuthExpiredToken() {
        // arrange
        var db = Substitute.For<ITokenRepository>();
        db.GetToken(new Token { Id = "valid" }).Returns(new Token { Id = "valid", UserId = 1, ValidUntil = DateTime.Now.Subtract(new TimeSpan(0, 1, 0)) });
        IAuth auth = new SimpleAuth();
        // act & assert
        Assert.Throws<ApiTokenExpiredException>(() => { auth.Auth(new Token { Id = "valid" }, 1); });
    }

    [Test]
    public void TestMediaAuth() {
        Assert.Pass();
    }

    [Test]
    public void TestRatingAuth() {
        Assert.Pass();
    }
    */
}
