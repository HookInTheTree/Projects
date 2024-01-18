using Microsoft.Extensions.Caching.Memory;
using Projects.Infrastructure.Common;
using Projects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Infrastructure.Project
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IMemoryCache cache;
        private readonly IIdsListCacheService idsService;

        private const string entitiesIdsListKey = "projects";
        private const string entityKey = "project_{0}";
        public ProjectRepository(IMemoryCache mem, IIdsListCacheService idsService)
        {
            cache = mem;
            this.idsService = idsService;
        }

        public Models.Project Create(Models.Project project)
        {
            var newProjectId = GetAll().Max(x => x.Id) + 1;

            var newProject = new Models.Project()
            {
                Id = newProjectId,
                Name = project.Name,
                Description = project.Description,
            };


            var key = string.Format(entityKey, newProject.Id);
            cache.Set<Models.Project>(key, newProject);

            idsService.AddId(newProject.Id, entitiesIdsListKey);

            return newProject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Models.Project Delete(Models.Project project)
        {
            var key = string.Format(entityKey, project.Id);

            if (!cache.TryGetValue<Models.Project>(key, out var result))
            {
                throw new ArgumentException($"Project with id - {project.Id} does not exist!");
            }

            cache.Remove(key);

            idsService.RemoveId(project.Id, entitiesIdsListKey);

            return result;
        }

        public ICollection<Models.Project> GetAll()
        {
            var ids = idsService.GetIds<int>(entitiesIdsListKey);
            
            var projects = new List<Models.Project>();

            foreach (var id in ids)
            {
                var project = GetById(id);
                projects.Add(project);
            }
            return projects;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Models.Project GetById(int projectId)
        {
            var key = string.Format(entityKey, projectId);

            if (cache.TryGetValue<Models.Project>(key, out var result))
            {
                throw new ArgumentException($"Project with id - {projectId} does not exist!");
            }
            return result;

        }

        public Models.Project Update(Models.Project project)
        {
            var key = string.Format(entityKey, project.Id);
            cache.Set<Models.Project>(key, project);
            return project;
        }
    }
}
