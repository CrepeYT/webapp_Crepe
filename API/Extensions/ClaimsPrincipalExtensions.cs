using System;
using System.Security.Claims;

namespace API.Extensions;
#nullable disable
public static class ClaimsPrincipalExtensions
{
    public static string GetUsername(this ClaimsPrincipal user) => user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}