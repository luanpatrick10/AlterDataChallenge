using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

public static class RoleAuthorization
{
    public static async Task RequireRole(HttpContext context, string requiredRole, Func<Task> next)
    {
        var role = context.Items["Role"] as string;
        if (role != requiredRole)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Forbidden: Insufficient role");
            return;
        }
        await next();
    }
}
