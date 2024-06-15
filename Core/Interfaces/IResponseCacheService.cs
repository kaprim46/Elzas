namespace Core.Interfaces
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cashKey, object response, TimeSpan timeToLive);
        Task<string> GetCachedResponseAsync(string cashKey);
    }
}