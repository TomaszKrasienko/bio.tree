using bio.tree.server.application.Services;
using bio.tree.server.domain.Models;
using Microsoft.AspNetCore.Identity;

namespace bio.tree.server.infrastructure.Security;

internal sealed class PasswordManager(IPasswordHasher<User> passwordHasher) : IPasswordManager
{
    public string Secure(string password)
        => passwordHasher.HashPassword(default, password);

    public bool IsValidPassword(string password, string securedPassword)
        => passwordHasher.VerifyHashedPassword(default, securedPassword, password) == PasswordVerificationResult.Success;
}