using bio.tree.server.infrastructure.DAL.Documents.Mappers;
using bio.tree.server.tests.shared.Factories.Documents;
using Shouldly;
using Xunit;

namespace bio.tree.server.infrastructure.tests.DAL.Mappers;

public sealed class DtoMapperExtensionsTests
{
    [Fact]
    public void AsDto_GivenUserDocument_ShouldReturnUserDto()
    {
        //arrange
        var userDocument = UserDocumentFactory.Get(1, 1).Single();
        
        //act
        var user = userDocument.AsDto();
        
        //assert
        
        user.Id.ShouldBe(userDocument.Id);
        user.FirstName.ShouldBe(userDocument.FirstName);
        user.LastName.ShouldBe(userDocument.LastName);
        user.Nickname.ShouldBe(userDocument.Nickname);
        user.UserLinks.First().Id.ShouldBe(userDocument.UserLinks.First().Id);
        user.UserLinks.First().Url.ShouldBe(userDocument.UserLinks.First().Url);
        user.UserLinks.First().PlatformId.ShouldBe(userDocument.UserLinks.First().PlatformId);
    }
    
    [Fact]
    public void AsDto_GivenUserDocumentWithoutUserLinks_ShouldReturnUserDto()
    {
        //arrange
        var userDocument = UserDocumentFactory.Get(1, 0).Single();
        
        //act
        var user = userDocument.AsDto();
        
        //assert
        
        user.Id.ShouldBe(userDocument.Id);
        user.FirstName.ShouldBe(userDocument.FirstName);
        user.LastName.ShouldBe(userDocument.LastName);
        user.Nickname.ShouldBe(userDocument.Nickname);
        user.UserLinks.ShouldBeNull();
    }
}