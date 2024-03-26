using bio.tree.server.application.CQRS.Users.Commands.SignUp;
using bio.tree.server.application.Exceptions;
using bio.tree.server.application.Services;
using bio.tree.server.domain.Repositories;
using NSubstitute;
using Shouldly;
using Xunit;

namespace bio.tree.server.application.tests.CQRS.Users.Commands;

public sealed class SignUpCommandHandlerTests
{
    private Task Act(SignUpCommand command) => _handler.HandleAsync(command, default);
    
    [Fact]
    public async Task HandleAsync_GivenNotExistingEmail_ShouldAddUser()
    {
        //arrange
        var securedPassword = "secured_password";
        var command = new SignUpCommand(Guid.NewGuid(), "test@test.pl", "first_name",
            "last_name", "nickname", "User","password");
        _userRepository.ExistsAsync(command.Email).Returns(false);
        _passwordManager.Secure(command.Password).Returns(securedPassword);
        
        //act
        await Act(command);
        
        //assert
        await _userRepository.Received(1).AddAsync(Arg.Is<domain.Models.User>(arg
            => arg.Id == command.Id
               && arg.Email == command.Email
               && arg.FullName.FirstName == command.FirstName
               && arg.FullName.LastName == command.LastName
               && arg.Nickname == command.Nickname
               && arg.Password == securedPassword
               && !string.IsNullOrWhiteSpace(arg.VerificationToken.Token)
               && arg.VerificationToken.ConfirmationDate == null));
    }
    
    [Fact]
    public async Task HandleAsync_GivenExistingEmail_ShouldThrowEmailAlreadyRegisteredException()
    {
        //arrange
        var securedPassword = "secured_password";
        var command = new SignUpCommand(Guid.NewGuid(), "test@test.pl", "first_name",
            "last_name", "nickname", "User","password");
        _userRepository.ExistsAsync(command.Email).Returns(true);
        _passwordManager.Secure(command.Password).Returns(securedPassword);
        
        //act
        var exception = await Record.ExceptionAsync(async () => await Act(command));
        
        //assert
        exception.ShouldBeOfType<EmailAlreadyRegisteredException>();
    }

    #region arrange

    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly SignUpCommandHandler _handler;

    public SignUpCommandHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordManager = Substitute.For<IPasswordManager>();
        _handler = new SignUpCommandHandler(_userRepository, _passwordManager);
    }
    #endregion
}