using System.IdentityModel.Tokens.Jwt;

namespace Autodroid.SDK.Authorization;

public interface IJwtConsumerService
{
    JwtSecurityToken ValidateToken(string token);
}