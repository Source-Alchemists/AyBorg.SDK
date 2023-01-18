namespace AyBorg.SDK.Authorization;

public interface IJwtGenerator
{
    string GenerateToken(string userName, IEnumerable<string> roles);
}
