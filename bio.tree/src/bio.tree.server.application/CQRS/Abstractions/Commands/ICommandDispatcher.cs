namespace bio.tree.server.application.CQRS.Abstractions.Commands;

public interface ICommandDispatcher
{
    Task SendAsync<TCommand>(TCommand command, CancellationToken token) where TCommand : class, ICommand;
}