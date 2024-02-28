using bio.tree.server.application.Services;

namespace bio.tree.server.integration.tests._Helpers;

public class TestsPasswordManager : IPasswordManager
{
    public string Secure(string password)
        => password;

    public bool IsValidPassword(string password, string securedPassword)
        => password == securedPassword;
}