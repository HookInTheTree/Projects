using Projects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Business.WorkItem
{
    public class WorkItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public WorkItemStatus WorkItemStatus { get; set; }
        public string Project { get; set; }
    }
}
