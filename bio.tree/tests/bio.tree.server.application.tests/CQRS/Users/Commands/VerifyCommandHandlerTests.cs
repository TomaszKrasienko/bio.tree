using bio.tree.server.application.CQRS.Users.Commands.Verify;
using bio.tree.server.application.Exceptions;
using bio.tree.server.application.Services;
using bio.tree.server.domain.Models;
using bio.tree.server.domain.Repositories;
using bio.tree.server.tests.shared.Factories;
using NSubstitute;
using Shouldly;
using Xunit;

namespace bio.tree.server.application.tests.CQRS.Users.Commands;

public sealed class VerifyCommandHandlerTests
{
    Task Act(VerifyCommand command) => _handler.HandleAsync(command, default);
    
    [Fact]
    public async Task HandleAsync_GivenNotExistingToken_ShouldThrowTokenDoesNotExistsException()
    {
        //arrange
        var token = "invalid_token";
        await _userRepository.GetByVerificationTokenAsync(token);
        var command = new VerifyCommand(token);
        
        //act
        var exception = await Record.ExceptionAsync(async () => await Act(command));
        
        //assert
        exception.ShouldBeOfType<TokenDoesNotExistsException>();
    }
    
    [Fact]
    public async Task HandleAsync_GivenExistingToken_ShouldVerifyUser()
    {
        //arrange
        var token = _user.VerificationToken.Token;
        _userRepository
            .GetByVerificationTokenAsync(token)
            .Returns(_user);
        var command = new VerifyCommand(token);
        
        //act
        await Act(command);
        
        //assert
        _user.VerificationToken.ConfirmationDate.ShouldNotBeNull();
        await _userRepository.Received(1).UpdateAsync(_user);
    }

    #region arrange

    private readonly IUserRepository _userRepository;
    private readonly IClock _clock;
    private readonly VerifyCommandHandler _handler;
    
    private readonly User _user;
    
    public VerifyCommandHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _clock = TestClock.Get();
        _handler = new VerifyCommandHandler(_userRepository, _clock);
        _user = User.Create(Guid.NewGuid(), "test@test.pl", "first_name", "last_name",
            "nickname", "User","test_pass");
    }
    #endregion
}