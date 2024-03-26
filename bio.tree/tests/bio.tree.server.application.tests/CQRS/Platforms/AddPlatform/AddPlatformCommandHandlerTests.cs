using bio.tree.server.application.CQRS.Platforms.Commands.AddPlatform;
using bio.tree.server.application.Exceptions;
using bio.tree.server.domain.Models;
using bio.tree.server.domain.Repositories;
using NSubstitute;
using Shouldly;
using Xunit;

namespace bio.tree.server.application.tests.CQRS.Platforms.AddPlatform;

public sealed class AddPlatformCommandHandlerTests
{
    private Task Act(AddPlatformCommand command) => _handler.HandleAsync(command, default);

    [Fact]
    public async Task Handle_GivenAddPlatformCommandForNotExistingPlatform_ShouldAddByRepository()
    {
        //arrange
        var command = new AddPlatformCommand(Guid.NewGuid(), "new_platform", "new_icon");
        _platformRepository
            .ExistsAsync(Arg.Is<string>(arg => arg == command.Name))
            .Returns(false);

        //act
        await Act(command);
        
        //assert
        await _platformRepository
            .Received(1)
            .AddAsync(Arg.Is<Platform>(arg
                => arg.Id == command.Id
                   && arg.Name == command.Name
                   && arg.Icon == command.Icon));
    }

    [Fact]
    public async Task Handle_GivenExistingPlatformName_ShouldThrowPlatformAlreadyExists()
    {
        //arrange
        var command = new AddPlatformCommand(Guid.NewGuid(), "new_platform", "new_icon");
        _platformRepository
            .ExistsAsync(Arg.Is<string>(arg => arg == command.Name))
            .Returns(true);

        //act
        var exception = await Record.ExceptionAsync(async () => await Act(command));
        
        //assert
        exception.ShouldBeOfType<PlatformAlreadyExistsException>();
    }
    
    #region arrange
    private readonly IPlatformRepository _platformRepository;
    private readonly AddPlatformCommandHandler _handler;
    
    public AddPlatformCommandHandlerTests()
    {
        _platformRepository = Substitute.For<IPlatformRepository>();
        _handler = new AddPlatformCommandHandler(_platformRepository);
    }
    #endregion
}