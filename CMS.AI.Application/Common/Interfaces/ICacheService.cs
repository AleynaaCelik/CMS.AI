using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Application.Common.Interfaces
{

    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class;
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiry = null, CancellationToken cancellationToken = default) where T : class;
        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default) where T : class;
        Task RemoveAsync(string key, CancellationToken cancellationToken = default);
        Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default);
    }
}
