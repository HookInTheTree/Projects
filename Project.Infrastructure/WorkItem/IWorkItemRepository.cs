using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Infrastructure.WorkItem
{
    public interface IWorkItemRepository
    {
        Models.WorkItem Create(Models.WorkItem workItem);
        Models.WorkItem Update(Models.WorkItem workItem);
        Models.WorkItem Delete(Models.WorkItem workItem);
        ICollection<Models.WorkItem> GetAll();
        Models.WorkItem GetById(int id);
    }
}
