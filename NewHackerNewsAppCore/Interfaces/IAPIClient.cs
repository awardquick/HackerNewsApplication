using System;
using System.Threading.Tasks;

namespace HackerNewsAppCore
{
    public interface IAPIClient
    {
        //Get request to to external api

        Task<T> GetAsync<T>(string url);

        //Cached request 

        Task<T> GetCachedAsync<T>(string url, TimeSpan expirationTime);
    }
}
