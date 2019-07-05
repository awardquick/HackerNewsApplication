using HackerNewsAppCore;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NewHackerNewsAppInfrastructure.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NewHackerNewsAppInfrastructure.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewHackerNewsAppInfrastructure.Models;

namespace NewHackerNewsAppInfrastructureTest
{
    [TestClass]
    public class HackerNewsServiceTest 
    {

        private HackerNewsService hackerNews;
        private Mock<IAPIClient> client;

        public HackerNewsServiceTest()
        {
            var mapperMock = new Mapper(new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ItemMappingProfile());
            }));
            client = new Mock<IAPIClient>();
            hackerNews = new HackerNewsService(client.Object, mapperMock);
        }

        [TestMethod]
        public async Task GetNewsAsync_WithArgLessThan1_ThrowsExpection()
        {
            await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
                hackerNews.GetNewsAsync(0));

        }

        [TestMethod]
        public async Task GetNewsAsync_ReturnsMaxArticleAmt_AndNothingMore()
        {
            client.Setup(x => x.GetAsync<int[]>(It.IsAny<string>()))
                .ReturnsAsync(new int[500]);

            await hackerNews.GetNewsAsync(501);

            client.Verify(x => x.GetCachedAsync<Item>(It.IsAny<string>(), It.IsAny<TimeSpan>()), Times.Exactly(500));

        }
        [TestMethod]
        public async Task GetNewsAsync_Returns_AmountPassedInArg()
        {
            client.Setup(x => x.GetAsync<int[]>(It.IsAny<string>()))
                .ReturnsAsync(new int[500]);

            await hackerNews.GetNewsAsync(30);

            client.Verify(x => x.GetCachedAsync<Item>(It.IsAny<string>(), It.IsAny<TimeSpan>()), Times.Exactly(30));

        }
    }
}
