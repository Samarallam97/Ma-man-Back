namespace OnlineSchoolForKids.Core.Services.Interfaces;

public interface ICacheService
{
	Task CacheResponseAsync(string key, object response, TimeSpan timeOut);
	Task<string?> GetCachedResponseAsync(string key);
}
