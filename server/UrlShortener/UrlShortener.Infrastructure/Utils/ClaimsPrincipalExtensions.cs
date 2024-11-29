using System.Security.Claims;
using UrlShortener.Infrastructure.Constants;

namespace UrlShortener.Infrastructure.Utils;

public static class ClaimsPrincipalExtensions
{
    public static bool IsInAnyRole(this ClaimsPrincipal claimsPrincipal)
    {
        bool isInAnyRole = false;
        foreach (var role in IdentityRoles.GetAllRoles())
        {
            if (claimsPrincipal.IsInRole(role) is true)
            {
                isInAnyRole = true;
            }
        }

        return isInAnyRole;
    }
}