using bio.tree.server.application.CQRS.Abstractions.Commands;

namespace bio.tree.server.application.CQRS.Platforms.Commands.AddPlatform;

public record AddPlatformCommand(Guid Id, string Name, string Icon) : ICommand;