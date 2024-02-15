using Projects.Business.Project.DTO;

namespace Projects.Business.Project
{
    public interface IProjectManager
    {
        ProjectModel CreateProject(ProjectCreationModel model);
        ProjectModel UpdateProject(ProjectUpdateModel model);
        ProjectModel DestroyProject(int id);
        ProjectModel GetProject(int id);
        ICollection<ProjectModel> GetAllProjects();
    }
}
