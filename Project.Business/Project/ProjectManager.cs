using Projects.Business.Project.DTO;
using Projects.Business.WorkItem;
using Projects.Infrastructure.Project;
using Projects.Infrastructure.WorkItem;
using Projects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Business.Project
{
    public class ProjectManager : IProjectManager
    {
        private readonly IProjectRepository projectRepository;
        private readonly IWorkItemRepository workItemRepository;
        public ProjectManager(IProjectRepository projectRepository, IWorkItemRepository workItemRepository)
        {
            this.projectRepository = projectRepository;
            this.workItemRepository = workItemRepository;
        }

        public ProjectModel CreateProject(ProjectCreationModel model)
        {
            var createdModel = projectRepository.Create(new()
            {
                Name = model.Name,
                Description = model.Description
            });
            
            return new ProjectModel()
            {
                Id = createdModel.Id,
                Name = createdModel.Name,
                Description = createdModel.Description,
                WorkItems = new List<WorkItemModel>()
            };
        }

        public ProjectModel DestroyProject(int id)
        {
            var deletedModel = projectRepository.Delete(id);

            var workItems = workItemRepository.GetAll().Where(x => x.ProjectId == deletedModel.Id);

            //Bad practice, but in this project this is ok. 
            foreach (var workItem in workItems)
            {
                workItemRepository.Delete(workItem);
            }

            var projectWorkItems = workItems
               .Select(x => new WorkItemModel()
               {
                   Id = x.Id,
                   Name = x.Name,
                   Description = x.Description,
                   WorkItemStatus = x.WorkItemStatus,
                   Project = deletedModel.Name
               }).ToList();

            return new ProjectModel()
            {
                Id = deletedModel.Id,
                Name = deletedModel.Name,
                Description = deletedModel.Description,
                WorkItems = projectWorkItems
            };
        }

        public ICollection<ProjectModel> GetAllProjects()
        {
            var projects = projectRepository.GetAll();
            var workItems = workItemRepository.GetAll();

            var result = new List<ProjectModel>();
            foreach (var project in projects)
            {
                var projectWorkItems = workItems
                .Where(x => x.ProjectId == project.Id)
                .Select(x => new WorkItemModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    WorkItemStatus = x.WorkItemStatus,
                    Project = project.Name
                }).ToList();

                result.Add(new ProjectModel()
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    WorkItems = projectWorkItems
                });
            }

            return result;
        }

        public ProjectModel GetProject(int id)
        {
            var project = projectRepository.GetById(id);

            var projectWorkItems = workItemRepository.GetAll()
                .Where(x => x.ProjectId == project.Id)
                .Select(x => new WorkItemModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    WorkItemStatus = x.WorkItemStatus,
                    Project = project.Name
                }).ToList();

            return new ProjectModel()
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                WorkItems = projectWorkItems
            };
        }

        public ProjectModel UpdateProject(ProjectUpdateModel model)
        {
            var updatedProject = projectRepository.Update(new()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            });

            var projectWorkItems = workItemRepository.GetAll()
                .Where(x => x.ProjectId == updatedProject.Id)
                .Select(x => new WorkItemModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    WorkItemStatus = x.WorkItemStatus,
                    Project = updatedProject.Name
                }).ToList();

            return new ProjectModel()
            {
                Id = updatedProject.Id,
                Name = updatedProject.Name,
                Description = updatedProject.Description,
                WorkItems = projectWorkItems
            };
        }
    }
}
