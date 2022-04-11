using System.Security.Claims;

namespace ToDoApi.Util;

public static class User
{
    public static string GetUserId(HttpContext httpContext)
    {
        var claim = httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
        return claim!.Value;
    }
}