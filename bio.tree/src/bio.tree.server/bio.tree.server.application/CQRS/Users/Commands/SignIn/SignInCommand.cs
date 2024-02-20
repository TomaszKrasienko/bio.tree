using bio.tree.server.application.CQRS.Abstractions.Commands;

namespace bio.tree.server.application.CQRS.Users.Commands.SignIn;

public record SignInCommand(string Email, string Password) : ICommand;