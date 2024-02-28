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
        var exception = Record.Exception(() => Platform.Create(Guid.Empty, "platform_name"));
        
        //assert
        exception.ShouldBeOfType<EmptyEntityIdException>();
    }
    
    [Fact]
    public void Create_GivenEmptyName_ShouldThrowEmptyPlatformNameException()
    {
        //act
        var exception = Record.Exception(() => Platform.Create(Guid.NewGuid(), string.Empty));
        
        //assert
        exception.ShouldBeOfType<EmptyPlatformNameException>();
    }
}