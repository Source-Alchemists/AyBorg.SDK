using System.IdentityModel.Tokens.Jwt;

namespace Atomy.SDK.Authorization;

public interface IJwtConsumerService
{
    JwtSecurityToken ValidateToken(string token);
}