using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AyBorg.SDK.Authorization;

public sealed class JwtGenerator : ITokenGenerator
{
    private readonly byte[] _secretKey;

    public JwtGenerator(IOptions<SecurityConfiguration> configuration)
    {
        _secretKey = Encoding.ASCII.GetBytes(configuration.Value.PrimarySharedKey.KeyValue);
    }

    public string GenerateUserToken(string userName, IEnumerable<string> roles)
    {
        return GenerateToken(userName, roles);
    }

    public string GenerateServiceToken(string serviceName, IEnumerable<string> roles)
    {
        return GenerateToken(serviceName, roles);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string GenerateToken(string name, IEnumerable<string> roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = new List<Claim> {
                new(ClaimTypes.Name, name),
            };
        if (roles.Any())
        {
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        }
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secretKey), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
