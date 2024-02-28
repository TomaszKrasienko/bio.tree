namespace bio.tree.server.application.Services;

public interface IPasswordManager
{
    string Secure(string password);
    bool IsValidPassword(string password, string securedPassword);
}