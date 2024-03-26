using bio.tree.server.application.CQRS.Abstractions.Commands;
using bio.tree.server.application.Exceptions;
using bio.tree.server.application.Services;
using bio.tree.server.domain.Models;
using bio.tree.server.domain.Repositories;

namespace bio.tree.server.application.CQRS.Users.Commands.SignUp;

internal sealed class SignUpCommandHandler(
    IUserRepository userRepository, 
    IPasswordManager passwordManager) 
    : ICommandHandler<SignUpCommand>
{
    public async Task HandleAsync(SignUpCommand command, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsAsync(command.Email))
        {
            throw new EmailAlreadyRegisteredException(command.Email);
        }

        var securedPassword = passwordManager.Secure(command.Password);
        var user = User.Create(command.Id, command.Email, command.FirstName, command.LastName,
            command.Nickname, command.Role, securedPassword);
        await userRepository.AddAsync(user);
    }
}