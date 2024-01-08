using System.Security.Claims;

namespace DoctorWhen.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserName(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Name)?.Value;
    }

    public static long GetUserId(this ClaimsPrincipal user)
    {
        string str = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        return long.Parse(str);
    }
}