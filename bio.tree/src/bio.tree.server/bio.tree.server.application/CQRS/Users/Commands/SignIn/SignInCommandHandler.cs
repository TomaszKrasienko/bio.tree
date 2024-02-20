using bio.tree.server.application.CQRS.Abstractions.Commands;
using bio.tree.server.application.Services;
using bio.tree.server.domain.Exceptions;
using bio.tree.server.domain.Repositories;

namespace bio.tree.server.application.CQRS.Users.Commands.SignIn;

internal sealed class SignInCommandHandler(
    IUserRepository userRepository,
    IPasswordManager passwordManager,
    IAuthenticator authenticator,
    ITokenStorage tokenStorage): ICommandHandler<SignInCommand>
{
    public async Task HandleAsync(SignInCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(command.Email);
        if (user is null)
        {
            throw new UserNotFoundException(command.Email);
        }

        if (!user.CanBeLogged())
        {
            throw new UserCanNotBeLoggedException(command.Email);
        }

        if (!passwordManager.IsValidPassword(command.Password, user.Password))
        {
            throw new InvalidPasswordException(command.Email);
        }

        var token = authenticator.CreateToken(user.Id);
        tokenStorage.Set(token);
    }
}

internal class InvalidPasswordException(string email)
    : BioTreeException($"Invalid password for user with email: {email}");
public sealed class UserNotFoundException(string email)
    : BioTreeException($"User with email: {email} does not exists");

public sealed class UserCanNotBeLoggedException(string email)
    : BioTreeException($"User with email: {email} does not exists");