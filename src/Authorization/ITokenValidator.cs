using Microsoft.IdentityModel.Tokens;

namespace AyBorg.SDK.Authorization;

/// <summary>
/// Token validator interface
/// </summary>
/// <typeparam name="T">Security token type</typeparam>
public interface ITokenValidator<T> where T : SecurityToken
{
    /// <summary>
    /// Validate token
    /// </summary>
    /// <param name="token">Token</param>
    /// <param name="tokenObject">Token object</param>
    /// <returns>True if token is valid, otherwise false</returns>
    bool Validate(string token, out T tokenObject);
}
