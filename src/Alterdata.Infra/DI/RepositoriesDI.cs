using Alterdata.Domain.Repositories;
using Alterdata.Infra.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Alterdata.Infra.DI;

public static class RepositoriesDI
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProjectRepository, ProjectRepository>();
    }
}