using Microsoft.Extensions.DependencyInjection;
using Projects.Infrastructure.Project;
using Projects.Infrastructure.WorkItem;

namespace Projects.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IProjectRepository, ProjectRepository>()
                    .AddScoped<IWorkItemRepository, WorkItemRepository>();

            return services;
        }
    }
}
