using bio.tree.client.Auth.Abstractions;
using Blazored.LocalStorage;

namespace bio.tree.client.Auth;

internal sealed class AuthService
    (ILocalStorageService localStorage): IAuthService
{
    private const string Key = "user_token";
    
    public async Task SetToken(string token)
        => await localStorage.SetItemAsync(Key, token);

    public async Task<string> GetToken()
        => await localStorage.GetItemAsync<string>(Key);
}