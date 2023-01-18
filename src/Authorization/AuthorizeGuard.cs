using Microsoft.AspNetCore.Http;

namespace AyBorg.SDK.Authorization;

public static class AuthorizeGuard
{
    public static void ThrowIfNotAuthorized(HttpContext httpContext, IEnumerable<string> allowedRoles)
    {
        System.Security.Claims.ClaimsPrincipal user = httpContext.User;
        if (allowedRoles != null && allowedRoles.Any() && !user.Claims.Any(claim => claim.Type.Equals("role") && allowedRoles.Contains(claim.Value)))
        {
            throw new UnauthorizedAccessException();
        }
    }
}
