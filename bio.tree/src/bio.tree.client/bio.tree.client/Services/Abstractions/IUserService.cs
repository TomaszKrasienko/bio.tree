using bio.tree.client.Models.Commands.User;

namespace bio.tree.client.Services.Abstractions;

public interface IUserService
{
    Task<HttpResponseMessage> SignUp(SignUpCommand command);
    Task<HttpResponseMessage> SignIn(SignInCommand command);
    Task<HttpResponseMessage> Verify(string token);
}