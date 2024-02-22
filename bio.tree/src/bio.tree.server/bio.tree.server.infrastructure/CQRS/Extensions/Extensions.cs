using bio.tree.server.application.CQRS.Abstractions.Commands;
using bio.tree.server.application.CQRS.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace bio.tree.server.infrastructure.CQRS.Extensions;

internal static class Extensions
{
    internal static IServiceCollection AddCqrs(this IServiceCollection services)
        => services
            .AddHandlers()
            .AddDispatchers();

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        services
            .Scan(a => a.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        services
            .Scan(a => a.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }

    private static IServiceCollection AddDispatchers(this IServiceCollection services)
        => services
            .AddScoped<ICommandDispatcher, CommandDispatcher>()
            .AddScoped<IQueryDispatcher, QueryDispatcher>();
}