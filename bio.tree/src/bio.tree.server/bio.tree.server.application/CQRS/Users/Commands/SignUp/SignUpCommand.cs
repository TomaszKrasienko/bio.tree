using bio.tree.server.application.CQRS.Abstractions.Commands;

namespace bio.tree.server.application.CQRS.Users.Commands.SignUp;

public record SignUpCommand(Guid Id, string Email, string FirstName,
    string LastName, string Nickname, string Password) : ICommand;