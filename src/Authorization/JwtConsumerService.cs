using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Autodroid.SDK.Authorization;

public sealed class JwtConsumerService : IJwtConsumerService
{
    private readonly ILogger<JwtConsumerService> _logger;
    private readonly IConfiguration _configuration;
    private readonly byte[] _secretKey;

    public JwtConsumerService(ILogger<JwtConsumerService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _secretKey = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Autodroid:Jwt:SecretKey"));
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