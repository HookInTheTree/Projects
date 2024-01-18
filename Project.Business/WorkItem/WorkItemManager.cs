using Projects.Infrastructure.Project;
using Projects.Infrastructure.WorkItem;
using Projects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Business.WorkItem
{
    public class WorkItemManager : IWorkItemManager
    {
        private readonly IWorkItemRepository workItemRepository;
        private readonly IProjectRepository projectRepository;
        
        public WorkItemManager(IWorkItemRepository workItemRepository, IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
            this.workItemRepository = workItemRepository;
        }

        public WorkItemModel CreateWorkItem(Models.WorkItem workItem)
        {
            var createdWorkItem = workItemRepository.Create(workItem);
            var project = projectRepository.GetById(workItem.Id);

            return new WorkItemModel()
            {
                Id = createdWorkItem.Id,
                Name = createdWorkItem.Name,
                Description = createdWorkItem.Description,
                WorkItemStatus = createdWorkItem.WorkItemStatus,
                Project = project.Name
            };
        }

        public WorkItemModel DeleteWorkItem(Models.WorkItem workItem)
        {
            var deletedWorkItem = workItemRepository.Delete(workItem);
            var project = projectRepository.GetById(workItem.Id);

            return new WorkItemModel()
            {
                Id = deletedWorkItem.Id,
                Name = deletedWorkItem.Name,
                Description = deletedWorkItem.Description,
                WorkItemStatus = deletedWorkItem.WorkItemStatus,
                Project = project.Name
            };
        }

        public ICollection<WorkItemModel> GetAllWorkItems()
        {
            var workItems = workItemRepository.GetAll();
            var projects = projectRepository.GetAll();

            return workItems.Select(x => new WorkItemModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                WorkItemStatus = x.WorkItemStatus,
                Project = projects.First(p => p.Id == x.ProjectId).Name
            }).ToList();
        }

        public WorkItemModel GetWorkItem(int id)
        {
            var workItem = workItemRepository.GetById(id);
            var project = projectRepository.GetById(workItem.Id);

            return new WorkItemModel()
            {
                Id = workItem.Id,
                Name = workItem.Name,
                Description = workItem.Description,
                WorkItemStatus = workItem.WorkItemStatus,
                Project = project.Name
            };
        }

        public WorkItemModel UpdateWorkItem(Models.WorkItem workItem)
        {
            var updatedWorkItem = workItemRepository.Update (workItem);
            var project = projectRepository.GetById(workItem.Id);

            return new WorkItemModel()
            {
                Id = updatedWorkItem.Id,
                Name = updatedWorkItem.Name,
                Description = updatedWorkItem.Description,
                WorkItemStatus = updatedWorkItem.WorkItemStatus,
                Project = project.Name
            };
        }
    }
}
