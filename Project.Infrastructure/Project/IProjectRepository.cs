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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        Models.Project Delete(int projectId);
        ICollection<Models.Project> GetAll();
        Models.Project GetById(int projectId);
    }
}
