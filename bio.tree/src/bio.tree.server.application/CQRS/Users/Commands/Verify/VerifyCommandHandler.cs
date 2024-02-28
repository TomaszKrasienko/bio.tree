using bio.tree.server.application.CQRS.Abstractions.Commands;
using bio.tree.server.application.Exceptions;
using bio.tree.server.application.Services;
using bio.tree.server.domain.Repositories;

namespace bio.tree.server.application.CQRS.Users.Commands.Verify;

internal sealed class VerifyCommandHandler(
    IUserRepository userRepository,
    IClock clock) : ICommandHandler<VerifyCommand>
{
    public async Task HandleAsync(VerifyCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByVerificationTokenAsync(command.Token);
        if (user is null)
        {
            throw new TokenDoesNotExistsException();
        }
        user.Verify(command.Token, clock.Now());
        await userRepository.UpdateAsync(user);
    }
}