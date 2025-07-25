using System.Reflection;
using Alterdata.Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Alterdata.Application;

public static class DependencyInjection
{
  public static void AddApplicationServices(this IServiceCollection services)
  {
    services.AddMediatR(config => {
      config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
      config.RegisterServicesFromAssembly(typeof(ProjectCreatedEvent).Assembly);
    });        
  }
}