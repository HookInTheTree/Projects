using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Infrastructure.Common
{
    /// <summary>
    /// Service that helps to use IMemoryCache like DB.
    /// </summary>
    /// P.S This is overengineering. The simplest way to do the same is Dictionary that registered as a singleton
    public class IdsListCacheService : IIdsListCacheService
    {
        private readonly IMemoryCache cache;
        public IdsListCacheService(IMemoryCache memory)
        {
            cache = memory;
        }

        public T AddId<T>(T id, string key)
        {
            var idsList = cache.Get<List<T>>(key);
            
            if (idsList == null)
            {
                idsList = new List<T>();
            }
            
            idsList.Add(id);

            cache.Set<List<T>>(key, idsList);
            return id;
        }

        public List<T> GetIds<T>(string key)
        {
            return cache.GetOrCreate<List<T>>(key, entry =>
            {
                return new List<T>();
            });
        }


        public T RemoveId<T>(T id, string key)
        {
            var idsList = cache.Get<List<T>>(key);

            if (idsList != null)
            {
                idsList.Remove(id);
            }

            cache.Set<List<T>>(key, idsList);
            return id;
        }
    }
}
