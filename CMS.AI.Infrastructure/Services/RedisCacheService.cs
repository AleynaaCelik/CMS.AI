using CMS.AI.Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CMS.AI.Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly JsonSerializerOptions _jsonOptions;


        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            var cachedValue = await _cache.GetStringAsync(key, cancellationToken);

            if (string.IsNullOrEmpty(cachedValue))
                return null;

            return JsonSerializer.Deserialize<T>(cachedValue, _jsonOptions);
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiry = null, CancellationToken cancellationToken = default) where T : class
        {
            var cachedValue = await GetAsync<T>(key, cancellationToken);

            if (cachedValue != null)
                return cachedValue;

            var newValue = await factory();

            await SetAsync(key, newValue, expiry, cancellationToken);

            return newValue;
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default) where T : class
        {
            var options = new DistributedCacheEntryOptions();

            if (expiry.HasValue)
                options.AbsoluteExpirationRelativeToNow = expiry;

            var serializedValue = JsonSerializer.Serialize(value, _jsonOptions);

            await _cache.SetStringAsync(key, serializedValue, options, cancellationToken);
        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            return _cache.RemoveAsync(key, cancellationToken);
        }

        public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
        {
            // Not: Bu özellik StackExchange.Redis üstünde bir extension metod gerektirebilir
            // Bu basit implementasyon şimdilik yeterli
            await Task.CompletedTask;
        }
    }
}
