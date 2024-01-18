using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Infrastructure.Project
{
    public interface IProjectRepository
    {
        Models.Project Create(Models.Project project);
        Models.Project Update(Models.Project project);
        Models.Project Delete(Models.Project project);
        ICollection<Models.Project> GetAll();
        Models.Project GetById(int projectId);
    }
}
