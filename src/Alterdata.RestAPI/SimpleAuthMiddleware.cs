using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SimpleAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Dictionary<string, string> _tokens;

    public SimpleAuthMiddleware(RequestDelegate next, Dictionary<string, string> tokens)
    {
        _next = next;
        _tokens = tokens;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value;
        if (path != null && (path.StartsWith("/swagger") || path.StartsWith("/favicon.ico")))
        {
            await _next(context);
            return;
        }

        var token = context.Request.Headers["X-Auth-Token"].ToString();
        if (string.IsNullOrEmpty(token) || !_tokens.ContainsKey(token))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }
        context.Items["Role"] = _tokens[token];
        await _next(context);
    }
}
