using bio.tree.server.domain.Exceptions;
using bio.tree.server.domain.Models;
using Shouldly;
using Xunit;

namespace bio.tree.server.domain.tests.Models;

public sealed class UserTests
{
    [Fact]
    public void AddLink_GivenNotExistedPlatformAndValidUrl_ShouldAddToUserLinks()
    {
        //arrange
        var userLinkId = Guid.NewGuid();
        var url = "test_url";
        
        //act
        _user.AddLink(userLinkId, Guid.NewGuid(), url);
        
        //assert
        var link = _user.UserLinks.FirstOrDefault(x => x.Id == userLinkId);
        link.ShouldNotBeNull();
        link.Url.Value.ShouldBe(url);
    }
    
    [Fact]
    public void AddLink_GivenExistedSamePlatformAndValidUrl_ShouldThrowUserLinkWithThisPlatformAlreadyExistsException()
    {
        //arrange
        var platformId = Guid.NewGuid();
        var url = "test_url";
        _user.AddLink(Guid.NewGuid(), platformId, url);
        
        //act
        var exception = Record.Exception(() => _user.AddLink(Guid.NewGuid(), platformId, url));
        
        //assert
        exception.ShouldBeOfType<UserLinkWithThisPlatformAlreadyExistsException>();
    }
    
    [Fact]
    public void AddLink_GivenInvalidUrl_ShouldThrowEmptyUrlException()
    {
        //act
        var exception = Record.Exception(() => _user.AddLink(Guid.NewGuid(), Guid.NewGuid(), string.Empty));
        
        //assert
        exception.ShouldBeOfType<EmptyUrlException>();
    }

    [Fact]
    public void Verify_GivenValidToken_ShouldAddConfirmationDate()
    {
        //arrange
        var token = _user.VerificationToken.Token;
        
        //act
        _user.Verify(token, new DateTimeOffset());
        
        //assert
        _user.VerificationToken.ConfirmationDate.ShouldNotBeNull();
    }
    
    [Fact]
    public void Verify_GivenInvalidToken_ShouldThrowInvalidVerificationTokenException()
    {
        //arrange
        var token = "invalid_token";
        
        //act
        var exception = Record.Exception(() => _user.Verify(token, new DateTimeOffset()));
        
        //assert
        exception.ShouldBeOfType<InvalidVerificationTokenException>();
    }

    [Fact]
    public void CanBeLogged_ForNotVerifiedUser_ShouldReturnFalse()
    {
        //act
        var result = _user.CanBeLogged();
        
        //assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void CanBeLogged_ForVerifiedUser_ShouldReturnTrue()
    {
        //act
        var token = _user.VerificationToken.Token;
        _user.Verify(token, new DateTimeOffset());
        
        //act
        var result = _user.CanBeLogged();
        
        //assert
        result.ShouldBeTrue();
    }
    
    #region arrange

    private readonly User _user;

    public UserTests()
    {
        _user = User.Create(Guid.NewGuid(), "test@test.pl", "first_name", "last_name",
            "nickname", "User", "test_pass");
    }

    #endregion
}