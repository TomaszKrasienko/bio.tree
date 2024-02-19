using bio.tree.server.domain.Models;
using Shouldly;
using Xunit;

namespace bio.tree.server.domain.tests.Models;

public sealed class UserTests
{
    [Fact]
    public void AddLink_GivenNotExistedPlatformValidUrl_ShouldAddToUserLinks()
    {
        //arrange
        var userLinkId = Guid.NewGuid();
        var url = "test_url";
        
        //act
        _user.AddLink(userLinkId, Guid.NewGuid(), url);
        
        //assert
        var link = _user.UserLinks.FirstOrDefault(x => x.UserLinkId == userLinkId);
        link.ShouldNotBeNull();
        link.Url.Value.ShouldBe(url);
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