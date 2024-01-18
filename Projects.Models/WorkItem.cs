using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Models
{
    public enum WorkItemStatus
    {
        New,
        Active,
        Done
    }

    public class WorkItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public WorkItemStatus WorkItemStatus { get; set; }
        public int ProjectId { get; set; }
    }
}
