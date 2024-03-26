using bio.tree.server.domain.Exceptions;
using bio.tree.server.domain.Models;
using Shouldly;
using Xunit;

namespace bio.tree.server.domain.tests.Models;

public sealed class CreatePlatformTests
{
    [Fact]
    public void Create_GivenEmptyEntityId_ShouldThrowEmptyEntityIdException()
    {
        //act
        var exception = Record.Exception(() => Platform.Create(Guid.Empty, "platform_name", "icon_name"));
        
        //assert
        exception.ShouldBeOfType<EmptyEntityIdException>();
    }
    
    [Fact]
    public void Create_GivenEmptyName_ShouldThrowEmptyPlatformNameException()
    {
        //act
        var exception = Record.Exception(() => Platform.Create(Guid.NewGuid(), string.Empty, "test_icon"));
        
        //assert
        exception.ShouldBeOfType<EmptyPlatformNameException>();
    }
    
    [Fact]
    public void Create_GivenEmptyIcon_ShouldThrowEmptyPlatformIconException()
    {
        //act
        var exception = Record.Exception(() => Platform.Create(Guid.NewGuid(), "test_name", string.Empty));
        
        //assert
        exception.ShouldBeOfType<EmptyPlatformIconException>();
    }
}