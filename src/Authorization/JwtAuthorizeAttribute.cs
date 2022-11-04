using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Atomy.SDK.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class JwtAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public string[] Roles { get; set; } = null!;

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        var user = context.HttpContext.User;
        if (Roles != null && Roles.Any() && !user.Claims.Any(claim => claim.Type.Equals("role") && Roles.Contains(claim.Value)))
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}