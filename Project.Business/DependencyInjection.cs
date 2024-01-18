using Microsoft.Extensions.DependencyInjection;
using Projects.Business.Project;
using Projects.Business.WorkItem;

namespace Projects.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddScoped<IProjectManager, ProjectManager>()
                    .AddScoped<IWorkItemManager, WorkItemManager>();

            return services;
        }
    }
}
