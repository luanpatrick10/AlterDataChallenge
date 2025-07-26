using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using System.Threading.Tasks;

public class RoleFilter : IEndpointFilter
{
    private readonly string _requiredRole;
    public RoleFilter(string requiredRole)
    {
        _requiredRole = requiredRole;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var httpContext = context.HttpContext;
        var role = httpContext.Items["Role"] as string;
        if (role != _requiredRole)
        {
            throw new UnauthorizedAccessException("Forbidden: Insufficient role");
        }
        return await next(context);
    }
}
