using Microsoft.Extensions.Caching.Memory;
using Projects.Infrastructure.Common;
using Projects.Models;

namespace Projects.Infrastructure.WorkItem
{
    public class WorkItemRepository : IWorkItemRepository
    {
        private readonly IMemoryCache cache;
        private readonly IIdsListCacheService idsService;

        private const string entitiesIdsListKey = "workItems";
        private const string entityKey = "workItem_{0}";

        public WorkItemRepository(IMemoryCache mem, IIdsListCacheService idsService)
        {
            cache = mem;
            this.idsService = idsService;
        }

        public Models.WorkItem Create(Models.WorkItem workItem)
        {
            var key = string.Format(entityKey, workItem.Id);
            cache.Set<Models.WorkItem>(key, workItem);

            idsService.AddId(workItem.Id, entitiesIdsListKey);

            return workItem;
        }

        public Models.WorkItem Delete(Models.WorkItem workItem)
        {
            var key = string.Format(entityKey, workItem.Id);

            if (!cache.TryGetValue<Models.WorkItem>(key, out var result))
            {
                throw new ArgumentException($"WorkItem with id - {workItem.Id} does not exist!");
            }

            cache.Remove(key);

            idsService.RemoveId(workItem.Id, entitiesIdsListKey);

            return result;
        }

        public Models.WorkItem GetById(int id)
        {
            var key = string.Format(entityKey, id);

            if (cache.TryGetValue<Models.WorkItem>(key, out var result))
            {
                throw new ArgumentException($"WorkItem with id - {id} does not exist!");
            }
            return result;
        }

        public ICollection<Models.WorkItem> GetAll()
        {
            var ids = idsService.GetIds<int>(entitiesIdsListKey);

            var workItems = new List<Models.WorkItem>();

            foreach (var id in ids)
            {
                var workItem = GetById(id);
                workItems.Add(workItem);
            }
            return workItems;
        }

        public Models.WorkItem Update(Models.WorkItem workItem)
        {
            var key = string.Format(entityKey, workItem.Id);
            cache.Set<Models.WorkItem>(key, workItem);
            return workItem;
        }
    }
}
