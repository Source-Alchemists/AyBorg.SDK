using System.IdentityModel.Tokens.Jwt;

namespace AyBorg.SDK.Authorization;

public interface IJwtConsumerService
{
    JwtSecurityToken ValidateToken(string token);
}