namespace bio.tree.client.Auth.Abstractions;

public interface IAuthService
{
    Task SetToken(string token);
    Task<string> GetToken();
}