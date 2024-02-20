namespace bio.tree.server.application.CQRS.Abstractions.Queries;

public interface IQueryDispatch
{
    Task<TResult> HandleAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken);
}