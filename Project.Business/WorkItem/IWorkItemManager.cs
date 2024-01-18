using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Business.WorkItem
{
    public interface IWorkItemManager
    {
        WorkItemModel CreateWorkItem(Models.WorkItem workItem);
        WorkItemModel UpdateWorkItem(Models.WorkItem workItem);
        WorkItemModel DeleteWorkItem(Models.WorkItem workItem);
        WorkItemModel GetWorkItem(int id);
        ICollection<WorkItemModel> GetAllWorkItems();
    }
}
