using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config => { config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
    }
}