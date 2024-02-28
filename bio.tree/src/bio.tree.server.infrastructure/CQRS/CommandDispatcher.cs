using bio.tree.server.application.CQRS.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace bio.tree.server.infrastructure.CQRS;

internal sealed class CommandDispatcher(IServiceProvider serviceProvider) : ICommandDispatcher
{
    public async Task SendAsync<TCommand>(TCommand command, CancellationToken token) where TCommand : class, ICommand
    {
        var scope = serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.HandleAsync(command, token);
    }
}