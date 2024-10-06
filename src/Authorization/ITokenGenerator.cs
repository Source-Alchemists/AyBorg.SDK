namespace AyBorg.SDK.Authorization;

/// <summary>
/// Token generator interface
/// </summary>
public interface ITokenGenerator
{
    /// <summary>
    /// Generate user token
    /// </summary>
    /// <param name="userName">User name</param>
    /// <param name="roles">Roles</param>
    /// <returns>Token</returns>
    string GenerateUserToken(string userName, IEnumerable<string> roles);

    /// <summary>
    /// Generate service token
    /// </summary>
    /// <param name="serviceName">Service name</param>
    /// <param name="roles">Roles</param>
    /// <returns>Token</returns>
    string GenerateServiceToken(string serviceName, IEnumerable<string> roles);
}
