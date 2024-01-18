namespace Projects.Business.Project
{
    public interface IProjectManager
    {
        ProjectModel CreateProject(Models.Project model);
        ProjectModel UpdateProject(Models.Project model);
        ProjectModel DestroyProject(Models.Project model);
        ProjectModel GetProject(int id);
        ICollection<ProjectModel> GetAllProjects();
    }
}
