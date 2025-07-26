using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;

public static class EndpointRoleExtensions
{
    private static async Task Authorize(HttpContext context, string requiredRole)
    {
        var role = context.Items["Role"] as string;
        if (role != requiredRole)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Forbidden: Insufficient role");
            throw new InvalidOperationException();
        }
    }

    public static RouteHandlerBuilder MapPostWithRole<T1>(this IEndpointRouteBuilder app, string pattern, string requiredRole, Func<HttpContext, T1, Task> handler)
    {
        return app.MapPost(pattern, async (HttpContext context, T1 arg1) =>
        {
            await Authorize(context, requiredRole);
            await handler(context, arg1);
        });
    }

    public static RouteHandlerBuilder MapPostWithRole<T1, T2>(this IEndpointRouteBuilder app, string pattern, string requiredRole, Func<HttpContext, T1, T2, Task> handler)
    {
        return app.MapPost(pattern, async (HttpContext context, T1 arg1, T2 arg2) =>
        {
            await Authorize(context, requiredRole);
            await handler(context, arg1, arg2);
        });
    }

    public static RouteHandlerBuilder MapPostWithRole<T1, T2, T3>(this IEndpointRouteBuilder app, string pattern, string requiredRole, Func<HttpContext, T1, T2, T3, Task> handler)
    {
        return app.MapPost(pattern, async (HttpContext context, T1 arg1, T2 arg2, T3 arg3) =>
        {
            await Authorize(context, requiredRole);
            await handler(context, arg1, arg2, arg3);
        });
    }

    public static RouteHandlerBuilder MapGetWithRole<T1>(this IEndpointRouteBuilder app, string pattern, string requiredRole, Func<HttpContext, T1, Task> handler)
    {
        return app.MapGet(pattern, async (HttpContext context, T1 arg1) =>
        {
            await Authorize(context, requiredRole);
            await handler(context, arg1);
        });
    }

    public static RouteHandlerBuilder MapGetWithRole<T1, T2>(this IEndpointRouteBuilder app, string pattern, string requiredRole, Func<HttpContext, T1, T2, Task> handler)
    {
        return app.MapGet(pattern, async (HttpContext context, T1 arg1, T2 arg2) =>
        {
            await Authorize(context, requiredRole);
            await handler(context, arg1, arg2);
        });
    }

    public static RouteHandlerBuilder MapGetWithRole<T1, T2, T3>(this IEndpointRouteBuilder app, string pattern, string requiredRole, Func<HttpContext, T1, T2, T3, Task> handler)
    {
        return app.MapGet(pattern, async (HttpContext context, T1 arg1, T2 arg2, T3 arg3) =>
        {
            await Authorize(context, requiredRole);
            await handler(context, arg1, arg2, arg3);
        });
    }

    public static RouteHandlerBuilder MapPutWithRole<T1>(this IEndpointRouteBuilder app, string pattern, string requiredRole, Func<HttpContext, T1, Task> handler)
    {
        return app.MapPut(pattern, async (HttpContext context, T1 arg1) =>
        {
            await Authorize(context, requiredRole);
            await handler(context, arg1);
        });
    }

    public static RouteHandlerBuilder MapPutWithRole<T1, T2>(this IEndpointRouteBuilder app, string pattern, string requiredRole, Func<HttpContext, T1, T2, Task> handler)
    {
        return app.MapPut(pattern, async (HttpContext context, T1 arg1, T2 arg2) =>
        {
            await Authorize(context, requiredRole);
            await handler(context, arg1, arg2);
        });
    }

    public static RouteHandlerBuilder MapPutWithRole<T1, T2, T3>(this IEndpointRouteBuilder app, string pattern, string requiredRole, Func<HttpContext, T1, T2, T3, Task> handler)
    {
        return app.MapPut(pattern, async (HttpContext context, T1 arg1, T2 arg2, T3 arg3) =>
        {
            await Authorize(context, requiredRole);
            await handler(context, arg1, arg2, arg3);
        });
    }
}
