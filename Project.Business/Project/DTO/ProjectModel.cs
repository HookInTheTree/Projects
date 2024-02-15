using Projects.Business.WorkItem;

namespace Projects.Business.Project.DTO
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<WorkItemModel> WorkItems { get; set; }
    }
}