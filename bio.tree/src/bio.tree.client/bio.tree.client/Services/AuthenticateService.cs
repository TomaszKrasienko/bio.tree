using System.Net;
using System.Net.Http.Headers;
using bio.tree.client.Auth.Abstractions;

namespace bio.tree.client.Services;

public abstract class AuthenticateService(IAuthService authService, HttpClient client)
{
    protected async Task<HttpClient> Authenticate()
    {
        var token = await authService.GetToken();
        if (string.IsNullOrWhiteSpace(token))
        {
            return client;
        }
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        return client;
    } 
    
}