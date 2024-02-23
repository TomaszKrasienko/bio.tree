using System.Net;
using bio.tree.server.application.CQRS.Abstractions.Commands;
using bio.tree.server.application.CQRS.Users.Commands.SignUp;
using Microsoft.AspNetCore.Mvc;

namespace bio.tree.server.api.Controllers;

[ApiController]
[Route("users")]
public sealed class UserController(ICommandDispatcher commandDispatcher) : ControllerBase
{
    [HttpPost("sign-up")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignUp(SignUpCommand command, CancellationToken cancellationToken)
    {
        await commandDispatcher.SendAsync(command with {Id = Guid.NewGuid()}, cancellationToken);
        return Accepted();
    }
    
}