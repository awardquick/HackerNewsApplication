using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HackerNewsAppCore;
using NewHackerNewsAppInfrastructure.Models;

namespace NewHackerNewsApp.Controllers
{
    [Route("api/[controller]")]
    public class ArticleController : Controller
    {
        private readonly IHackerNewsService articleService;
        
        public ArticleController(IHackerNewsService articleService)
        {
            this.articleService = articleService;
        }
        [HttpGet("[action]/{articleNumber}")]
        public async Task <IEnumerable<Article>> GetArticles(int articleNumber)
        {
            return await articleService.GetNewsAsync(articleNumber);
        }

    }
}
