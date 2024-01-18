using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Infrastructure.Common
{
    public interface IIdsListCacheService
    {
        List<T> GetIds<T>(string key);
        T RemoveId<T>(T id, string key);
        T AddId<T>(T id, string key);
    }
}
