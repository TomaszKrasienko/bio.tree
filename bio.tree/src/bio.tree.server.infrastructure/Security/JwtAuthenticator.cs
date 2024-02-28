
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using bio.tree.server.application.Services;
using bio.tree.server.infrastructure.Security.Configuration.Models;
using bio.tree.shared.DTO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace bio.tree.server.infrastructure.Security;

internal sealed class JwtAuthenticator : IAuthenticator
{
    private readonly IClock _clock;
    private readonly JwtOptions _options;
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

    public JwtAuthenticator(IClock clock, IOptions<JwtOptions> options)
    {
        _clock = clock;
        _options = options.Value;
        _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(options.Value.SigningKey)),SecurityAlgorithms.HmacSha256);
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    }
    
    public JwtTokenDto CreateToken(Guid userId)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Name, userId.ToString()),
            new Claim(ClaimTypes.Role, userId.ToString())
        };
        var now = _clock.Now();
        var expiry = now.Add(_options.Expiry);
        var jwt = new JwtSecurityToken(_options.Issuer, _options.Audience, claims,
            now.DateTime, expiry.DateTime, _signingCredentials);
        var token = _jwtSecurityTokenHandler.WriteToken(jwt);
        return new JwtTokenDto
        {
            Token = token
        };
    }
}