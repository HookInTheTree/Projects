using Microsoft.AspNetCore.Mvc;
using Projects.Business.Project;
using Projects.WebApi.ApiModels.Projects;

namespace Projects.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        readonly IProjectManager _projectManager;
        public ProjectsController(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }

        [HttpGet]
        public ActionResult GetAll(int skip = 0, int take = 20)
        {
            var data = _projectManager.GetAllProjects();

            var result = new
            {
                data = data.Skip(skip).Take(take).ToList(),
                total = data.Count,
                page = (skip / take) == 0 ? 1 : skip / take
            };
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            var model = _projectManager.GetProject(int.Parse(id));
            return Ok(model);
        }

        [HttpPost]
        public ActionResult Create(ProjectCreationRequest request)
        {
            var createdModel = _projectManager.CreateProject(new()
            {
                Name = request.Name,
                Description = request.Description
            });

            return StatusCode(201, createdModel);
        }

        [HttpPatch("{id}")]
        public ActionResult Update(string id, ProjectUpdateRequest request)
        {
            var updatedModel = _projectManager.UpdateProject(new()
            {
                Id = int.Parse(id),
                Name = request.Name,
                Description = request.Description
            });

            return Ok(updatedModel);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var deletedModel = _projectManager.DestroyProject(int.Parse(id));
            return StatusCode(204, deletedModel.Id);
        }
    }
}
