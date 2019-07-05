using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNewsAppCore
{
    public interface IHackerNewsService
    {
        Task<IEnumerable<Article>> GetNewsAsync(int number);
    }
}
