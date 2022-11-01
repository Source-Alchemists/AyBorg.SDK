using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Atomy.SDK.Authorization
{
    public class JwtProviderService : IJwtProviderService
    {
        private readonly IConfiguration _configuration;
        private readonly byte[] _secretKey;
        public JwtProviderService(IConfiguration configuration)
        {
            _configuration = configuration;
            _secretKey = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:SecretKey"));
        }

        public string GenerateToken(string userName, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, userName),
            };
            if(roles.Any())
            {
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}