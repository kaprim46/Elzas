using System.Text.Json;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Services
{
    public class ResponseCachService : IResponseCacheService
    {
        private readonly IDatabase _database;
        public ResponseCachService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task CacheResponseAsync(string cachKey, object response, TimeSpan timeToLive)
        {
            if(response == null) return;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var serialisedResponse = JsonSerializer.Serialize(response, options);

            await _database.StringSetAsync(cachKey, serialisedResponse, timeToLive);
        }

        public async Task<string> GetCachedResponseAsync(string cashKey)
        {
            var cachedResponse = await _database.StringGetAsync(cashKey);

            if(cachedResponse.IsNullOrEmpty) return null;

            return cachedResponse;
        }
    }
}