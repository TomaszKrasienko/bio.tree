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
    
    #region arrange

    private readonly User _user;

    public UserTests()
    {
        _user = User.Create(Guid.NewGuid(), "test@test.pl", "first_name", "last_name",
            "nickname", "test_pass");
    }

    #endregion
}