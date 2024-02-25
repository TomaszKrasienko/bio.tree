using bio.tree.server.infrastructure.DAL.Documents.Mappers;
using bio.tree.server.tests.shared.Factories.Documents;
using Shouldly;
using Xunit;

namespace bio.tree.server.infrastructure.tests.DAL.Mappers;

public sealed class EntityMapperExtensionsTests
{
    [Fact]
    public void AsEntity_GivenUserDocument_ShouldReturnUser()
    {
        //arrange
        var userDocument = UserDocumentFactory.Get(1, 1).Single();
        
        //act
        var user = userDocument.AsEntity();
        
        //assert
        
        user.Id.Value.ShouldBe(userDocument.Id);
        user.FullName.FirstName.ShouldBe(userDocument.FirstName);
        user.FullName.LastName.ShouldBe(userDocument.LastName);
        user.Nickname.Value.ShouldBe(userDocument.Nickname);
        user.Password.Value.ShouldBe(userDocument.Password);
        user.VerificationToken.Token.ShouldBe(userDocument.VerificationToken);
        user.VerificationToken.ConfirmationDate.ShouldBe(userDocument.VerificationDate);
        user.ResetToken.Token.ShouldBe(userDocument.ResetToken);
        user.ResetToken.Expire.ShouldBe(userDocument.ResetTokenExpire);
        user.UserLinks.First().Id.Value.ShouldBe(userDocument.UserLinks.First().Id);
        user.UserLinks.First().Url.Value.ShouldBe(userDocument.UserLinks.First().Url);
        user.UserLinks.First().PlatformId.Value.ShouldBe(userDocument.UserLinks.First().PlatformId);
    }
    
    [Fact]
    public void AsEntity_GivenUserDocumentWithoutUserLinks_ShouldReturnUser()
    {
        //arrange
        var userDocument = UserDocumentFactory.Get(1, 0).Single();
        
        //act
        var user = userDocument.AsEntity();
        
        //assert
        user.Id.Value.ShouldBe(userDocument.Id);
        user.FullName.FirstName.ShouldBe(userDocument.FirstName);
        user.FullName.LastName.ShouldBe(userDocument.LastName);
        user.Nickname.Value.ShouldBe(userDocument.Nickname);
        user.Password.Value.ShouldBe(userDocument.Password);
        user.VerificationToken.Token.ShouldBe(userDocument.VerificationToken);
        user.VerificationToken.ConfirmationDate.ShouldBe(userDocument.VerificationDate);
        user.ResetToken.Token.ShouldBe(userDocument.ResetToken);
        user.ResetToken.Expire.ShouldBe(userDocument.ResetTokenExpire);
        user.UserLinks.ShouldBeNull();
    }
}