using HackerNewsAppCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNewsAppInfrastructure
{
    public class APIClient : IAPIClient
    {

        private readonly HttpClient client;
        private readonly IMemoryCache cache;

        public APIClient(HttpClient httpClient, IMemoryCache memCache)
        {
            client = httpClient;
            cache = memCache;
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var response = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<T> GetCachedAsync<T>(string url, TimeSpan sExpiration)
        {
            var response = await cache.GetOrCreateAsync(url, async itemEntry =>
            {
                itemEntry.SlidingExpiration = sExpiration;
                return await client.GetStringAsync(url);
            });

            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}
