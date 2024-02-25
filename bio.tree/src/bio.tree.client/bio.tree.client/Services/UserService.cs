using System.Net.Http.Headers;
using bio.tree.client.Auth.Abstractions;
using bio.tree.client.Models.Commands.User;
using bio.tree.client.Services.Abstractions;

namespace bio.tree.client.Services;

internal sealed class UserService(IAuthService service, HttpClient client) 
    : AuthenticateService(service, client), IUserService
{
    public async Task<HttpResponseMessage> SignUp(SignUpCommand command)
        => await client.PostAsJsonAsync<SignUpCommand>("/users/sign-up", command);

    public async Task<HttpResponseMessage> SignIn(SignInCommand command)
        => await client.PostAsJsonAsync<SignInCommand>("/users/sign-in", command);

    public async Task<HttpResponseMessage> Verify(string token)
        => await client.PostAsJsonAsync<VerifyCommand>("/users/verify", new VerifyCommand(token));
    
}