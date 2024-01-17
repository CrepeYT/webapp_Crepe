using System.Security.Claims;
#nullable disable
public static class ClaimsPrincipalExtensions
{
    public static string GetUsername(this ClaimsPrincipal user) => user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}