using bio.tree.server.infrastructure.DAL.Documents.Mappers;
using bio.tree.server.tests.shared.Factories.Entities;
using Shouldly;
using Xunit;

namespace bio.tree.server.infrastructure.tests.DAL.Mappers;

public sealed class DocumentMappersExtensionsTests
{
    [Fact]
    public void AsDocument_GivenUserWithAll_ShouldReturnUserDocument()
    {
        //arrange
        var user = UserFactory.Get(1, 1, true, true).Single();
        
        //act
        var document = user.AsDocument();
        
        //assert
        document.Id.ShouldBe(user.Id.Value);
        document.FirstName.ShouldBe(user.FullName.FirstName);
        document.LastName.ShouldBe(user.FullName.LastName);
        document.Nickname.ShouldBe(user.Nickname);
        document.Password.ShouldBe(user.Password);
        document.VerificationToken.ShouldBe(user.VerificationToken.Token);
        document.VerificationDate.ShouldBe(user.VerificationToken.ConfirmationDate);
        document.ResetToken.ShouldBe(user.ResetToken.Token);
        document.ResetTokenExpire.ShouldBe(user.ResetToken.Expire);
        document.UserLinks.First().Id.ShouldBe(user.UserLinks.First().Id.Value);
        document.UserLinks.First().Url.ShouldBe(user.UserLinks.First().Url);
        document.UserLinks.First().PlatformId.ShouldBe(user.UserLinks.First().PlatformId.Value);
    }

    [Fact]
    public void AsDocument_GivenOnlyRequire_ShouldReturnUserDocument()
    {
        //arrange
        var user = UserFactory.Get(1, 0, false, false).Single();
        
        //act
        var document = user.AsDocument();
        
        //assert
        document.Id.ShouldBe(user.Id.Value);
        document.FirstName.ShouldBe(user.FullName.FirstName);
        document.LastName.ShouldBe(user.FullName.LastName);
        document.Nickname.ShouldBe(user.Nickname);
        document.Password.ShouldBe(user.Password);
        document.VerificationToken.ShouldBeNull();
        document.VerificationDate.ShouldBeNull();
        document.ResetToken.ShouldBeNull();
        document.ResetTokenExpire.ShouldBeNull();
        document.UserLinks.ShouldBeNull();
    }
}