using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace AyBorg.SDK.Authorization;

[Obsolete("Use AyBorg.SDK.Authorization.JwtTValidator instead.")]
public sealed class JwtConsumer : IJwtConsumer
{
    private readonly ILogger<JwtConsumer> _logger;
    private readonly byte[] _secretKey;

    public JwtConsumer(ILogger<JwtConsumer> logger, IConfiguration configuration)
    {
        _logger = logger;
        _secretKey = Encoding.ASCII.GetBytes(configuration.GetValue<string>("AyBorg:Jwt:SecretKey"));
    }

    public JwtSecurityToken ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return null!;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_secretKey),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            return jwtToken;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to validate token");
            return null!;
        }
    }
}
