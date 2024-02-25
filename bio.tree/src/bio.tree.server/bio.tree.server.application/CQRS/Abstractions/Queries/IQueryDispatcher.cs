namespace bio.tree.server.application.CQRS.Abstractions.Queries;

public interface IQueryDispatcher
{
    Task<TResult> SendAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken);
}