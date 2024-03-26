using bio.tree.server.application.CQRS.Abstractions.Commands;
using bio.tree.server.application.Exceptions;
using bio.tree.server.domain.Models;
using bio.tree.server.domain.Repositories;

namespace bio.tree.server.application.CQRS.Platforms.Commands.AddPlatform;

internal sealed class AddPlatformCommandHandler(
    IPlatformRepository platformRepository) : ICommandHandler<AddPlatformCommand>
{
    public async Task HandleAsync(AddPlatformCommand command, CancellationToken cancellationToken)
    {
        if (await platformRepository.ExistsAsync(command.Name))
        {
            throw new PlatformAlreadyExistsException(command.Name);
        }

        var platform = Platform.Create(command.Id, command.Name, command.Icon);
        await platformRepository.AddAsync(platform);
    }
}