using bio.tree.server.application.CQRS.Users.Commands.SignIn;
using bio.tree.server.application.Exceptions;
using bio.tree.server.application.Services;
using bio.tree.server.domain.Models;
using bio.tree.server.domain.Repositories;
using bio.tree.shared.DTO;
using NSubstitute;
using Shouldly;
using Xunit;

namespace bio.tree.server.application.tests.CQRS.Users.Commands;

public sealed class SignInCommandHandlerTests
{
    private Task Act(SignInCommand command) => _handler.HandleAsync(command, default);

    [Fact]
    public async Task Handle_GivenNotExistingEmail_ShouldThrowUserNotFoundException()
    {
        //arrange
        var command = new SignInCommand(_user.Email, "password");
        
        //act
        var exception = await Record.ExceptionAsync(async() => await Act(command));
        
        //assert
        exception.ShouldBeOfType<AuthorizeUserNotFoundException>();
        _userRepository
            .ExistsAsync(_user.Email)
            .Returns(true);
    }

    [Fact]
    public async Task Handle_ForUserWhichCanNotBeLogged_ShouldThrowUserCanNotBeLoggedException()
    {
        //arrange
        var command = new SignInCommand(_user.Email, "password");
        _userRepository.GetByEmailAsync(command.Email)
            .Returns(_user);
        
        //act
        var exception = await Record.ExceptionAsync(async() => await Act(command));
        
        //assert
        exception.ShouldBeOfType<UserCanNotBeLoggedException>();
    }

    [Fact]
    public async Task Handle_GivenInvalidPassword_ShouldThrowInvalidPasswordException()
    {
        //arrange
        var command = new SignInCommand(_user.Email, "password");
        _userRepository.GetByEmailAsync(command.Email)
            .Returns(_user);
        _user.Verify(_user.VerificationToken.Token, new DateTimeOffset());
        _passwordManager
            .IsValidPassword(command.Password, _user.Password)
            .Returns(false);
        
        //act
        var exception = await Record.ExceptionAsync(async () => await Act(command));
        
        //assert
        exception.ShouldBeOfType<InvalidPasswordException>();
    }

    [Fact]
    public async Task Handle_GivenValidCredentials_ShouldSetTokenByTokenStorage()
    {
        //arrange
        var command = new SignInCommand(_user.Email, "password");
        _userRepository
            .GetByEmailAsync(command.Email)
            .Returns(_user);
        _user.Verify(_user.VerificationToken.Token, new DateTimeOffset());
        _passwordManager
            .IsValidPassword(command.Password, _user.Password)
            .Returns(true);
        var tokenDto = new JwtTokenDto()
        {
            Token = "test_returned_token"
        };
        _authenticator
            .CreateToken(_user.Id)
            .Returns(tokenDto);
        
        //act
        await Act(command);
        
        //assert
        _tokenStorage.Received(1).Set(tokenDto);
    }
    
    #region arrange
    
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IAuthenticator _authenticator;
    private readonly ITokenStorage _tokenStorage;
    private readonly SignInCommandHandler _handler;
    private readonly User _user;
    public SignInCommandHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordManager = Substitute.For<IPasswordManager>();
        _authenticator = Substitute.For<IAuthenticator>();
        _tokenStorage = Substitute.For<ITokenStorage>();
        _handler = new SignInCommandHandler(_userRepository,
            _passwordManager, _authenticator, _tokenStorage);
        
        _user = User.Create(Guid.NewGuid(), "test@test.pl", "first_name", "last_name",
            "nickname", "test_pass");
    }

    #endregion
}