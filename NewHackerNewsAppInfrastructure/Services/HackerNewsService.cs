using AutoMapper;
using HackerNewsAppCore;
using HackerNewsAppInfrastructure;
using NewHackerNewsAppInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewHackerNewsAppInfrastructure.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly IAPIClient client;
        private readonly IMapper _mapper;

        public HackerNewsService(IAPIClient client, IMapper mapper)
        {
            _mapper = mapper;
            this.client = client;
        }

        public async Task<IEnumerable<Article>> GetNewsAsync(int articleCount)
        {
            if (articleCount < 1)
            {
                throw new ArgumentException("The article number to retrieve must be more than 1");
            }

            var articleIds = await client.GetAsync<int[]>("https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty");

            articleCount = articleCount < articleIds.Length ? articleCount : articleIds.Length;

            var articles = await RetrieveArticles(articleCount, articleIds);

            return _mapper.Map<IEnumerable<Item>, IEnumerable<Article>>(articles); 
            
        }

        private async Task<IEnumerable<Item>> RetrieveArticles(int articleCount, IList<int> itemIds)
        {

            var items = new List<Item>();
            int i = 0;
            while(i < articleCount)
            {
                var itemId = itemIds[i];
                var item = await client.GetCachedAsync<Item>($"https://hacker-news.firebaseio.com/v0/item/{itemId}.json", TimeSpan.FromDays(2));

                if (item != null)
                {
                    items.Add(item);
                }

                i++;
            }
            return items;
        }

    }
}
