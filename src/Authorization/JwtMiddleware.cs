using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace AyBorg.SDK.Authorization;

public sealed class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IJwtConsumerService jwtConsumerService)
    {
        string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (string.IsNullOrEmpty(token))
        {
            await _next(context);
            return;
        }

        System.IdentityModel.Tokens.Jwt.JwtSecurityToken validatedToken = jwtConsumerService.ValidateToken(token);
        validatedToken?.Claims.ToList().ForEach(claim => context.User.AddIdentity(new ClaimsIdentity(new[] { claim }, "jwt")));
        await _next(context);
    }
}

public static class JwtMiddlewareExtensions
{
    public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtMiddleware>();
    }
}
