using System.Reflection.Metadata;
using bio.tree.server.application.CQRS.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace bio.tree.server.infrastructure.CQRS;

internal sealed class QueryDispatcher(IServiceProvider serviceProvider) : IQueryDispatcher
{
    public Task<TResult> SendAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);
        return ((Task<TResult>)handlerType
            .GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync))?
            .Invoke(handler, new object[] { query, cancellationToken }))!;
    }
}