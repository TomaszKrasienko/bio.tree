using bio.tree.server.application.CQRS.Abstractions.Commands;

namespace bio.tree.server.application.CQRS.Users.Commands.Verify;

public record VerifyCommand(string Token) : ICommand;