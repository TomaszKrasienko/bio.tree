using bio.tree.server.domain.Exceptions;
using bio.tree.server.domain.Models;
using bio.tree.server.domain.ValueObjects.User;
using Shouldly;
using Xunit;

namespace bio.tree.server.domain.tests.Models;

public sealed class CreateUserTests
{
    [Fact]
    public void Create_GivenEmptyEntityId_ShouldThrowEmptyEntityIdException()
    {
        //act
        var exception = Record.Exception(() => User.Create(Guid.Empty, "test@test.pl", 
            "test_first_name", "test_last_name", "test_nickname", "test_pass"));
        
        //assert
        exception.ShouldBeOfType<EmptyEntityIdException>();
    }
    
    [Fact]
    public void Create_GivenEmptyEmail_ShouldThrowEmptyEmailException()
    {
        //act
        var exception = Record.Exception(() => User.Create(Guid.NewGuid(), string.Empty, 
            "test_first_name", "test_last_name", "test_nickname", "test_pass"));
        
        //assert
        exception.ShouldBeOfType<EmptyEmailException>();
    }
    
    [Fact]
    public void Create_GivenInvalidEmail_ShouldThrowInvalidEmailException()
    {
        //act
        var exception = Record.Exception(() => User.Create(Guid.NewGuid(), "invalid_email", 
            "test_first_name", "test_last_name", "test_nickname", "test_pass"));
        
        //assert
        exception.ShouldBeOfType<InvalidEmailException>();
    }
    
    [Fact]
    public void Create_GivenEmptyFirstName_ShouldThrowEmptyFirstNameException()
    {
        //act
        var exception = Record.Exception(() => User.Create(Guid.NewGuid(), "test@test.pl", 
            string.Empty, "test_last_name", "test_nickname", "test_pass"));
        
        //assert
        exception.ShouldBeOfType<EmptyFirstNameException>();
    }
    
    [Fact]
    public void Create_GivenEmptyLastName_ShouldThrowEmptyLastNameException()
    {
        //act
        var exception = Record.Exception(() => User.Create(Guid.NewGuid(), "test@test.pl", 
            "test_first_name", string.Empty, "test_nickname", "test_pass"));
        
        //assert
        exception.ShouldBeOfType<EmptyLastNameException>();
    }
    
    [Fact]
    public void Create_GivenEmptyLastName_ShouldThrowEmptyNicknameException()
    {
        //act
        var exception = Record.Exception(() => User.Create(Guid.NewGuid(), "test@test.pl", 
            "test_first_name", "test_last_name", string.Empty, "test_pass"));
        
        //assert
        exception.ShouldBeOfType<EmptyNicknameException>();
    }
    
    [Fact]
    public void Create_GivenEmptyPassword_ShouldThrowEmptyPasswordException()
    {
        //act
        var exception = Record.Exception(() => User.Create(Guid.NewGuid(), "test@test.pl", 
            "test_first_name", "test_last_name", "test_nickname", string.Empty));
        
        //assert
        exception.ShouldBeOfType<EmptyPasswordException>();
    }

    [Fact]
    public void Create_GivenAllArguments_ShouldReturnUser()
    {
        //arrange
        var id = Guid.NewGuid();
        var email = "test@test.pl";
        var firstName = "test_first_name";
        var lastName = "test_last_name";
        var nickName = "test_nick_name";
        var pass = "test_pass";
        
        //act
        var user = User.Create(id, email, firstName, lastName, nickName,pass);
        
        //assert
        user.ShouldNotBeNull();
        user.Id.Value.ShouldBe(id);
        user.Email.Value.ShouldBe(email);
        user.FullName.FirstName.ShouldBe(firstName);
        user.FullName.LastName.ShouldBe(lastName);
        user.Nickname.Value.ShouldBe(nickName);
        user.Password.Value.ShouldBe(pass);
        user.VerificationToken.Token.ShouldNotBeNullOrWhiteSpace();
    }
}