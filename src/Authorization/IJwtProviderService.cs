namespace AyBorg.SDK.Authorization;

public interface IJwtProviderService
{
    string GenerateToken(string userName, IEnumerable<string> roles);
}