using System.IdentityModel.Tokens.Jwt;

namespace AyBorg.SDK.Authorization;

public interface IJwtConsumer
{
    JwtSecurityToken ValidateToken(string token);
}
