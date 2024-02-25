using System.Net;
using bio.tree.server.application.CQRS.Abstractions.Commands;
using bio.tree.server.application.CQRS.Users.Commands.SignIn;
using bio.tree.server.application.CQRS.Users.Commands.SignUp;
using bio.tree.server.application.CQRS.Users.Commands.Verify;
using bio.tree.server.application.Services;
using bio.tree.server.infrastructure.Contexts.Abstractions;
using bio.tree.shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bio.tree.server.api.Controllers;

[ApiController]
[Route("users")]
public sealed class UserController(
    ICommandDispatcher commandDispatcher,
    ITokenStorage tokenStorage,
    IIdentityContext identityContext) : ControllerBase
{
    // [HttpGet("me")]
    // [Authorize]
    // public async Task<ActionResult<UserDto>> GetMe()
    // {
    //     
    // }
    
    [HttpPost("sign-up")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignUp(SignUpCommand command, CancellationToken cancellationToken)
    {
        await commandDispatcher.SendAsync(command with {Id = Guid.NewGuid()}, cancellationToken);
        return Accepted();
    }

    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JwtTokenDto>> SignIn(SignInCommand command, CancellationToken cancellationToken)
    {
        await commandDispatcher.SendAsync(command, cancellationToken);
        var token = tokenStorage.Get();
        return Ok(token);
    }

    [HttpPost("verify")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Verify(VerifyCommand command, CancellationToken cancellationToken)
    {
        await commandDispatcher.SendAsync(command, cancellationToken);
        return Ok();
    }
}