using System;
using System.Threading.Tasks;
using HackerNewsAppInfrastructure;
using System.Net.Http;
using Moq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq.Protected;
using System.Threading;
using System.Net;
using Newtonsoft.Json;

namespace HackerNewsAppInfrastructureTest
{

    public class TestItem
    {
        public int Id { get; set; }
        public double Price { get; set; }

        public string Name { get; set; }

    }
    [TestClass]
    public class APIClientTest
    {
        private Mock<HttpMessageHandler> mockHandler;
        private Mock<IMemoryCache> memCache;
        private APIClient client;
        private const string testUrl = "https://abc123.com/api/testing";

        //[TestInitialize]
        public APIClientTest()
        {
            mockHandler = new Mock<HttpMessageHandler>();
            memCache = new Mock<IMemoryCache>();

        }
        [TestMethod]
        public async Task GetAsync_ReturnsNullResponse()
        {
            memCache = new Mock<IMemoryCache>();
            mockHandler = new Mock<HttpMessageHandler>();
            mockHandler.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()).ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("null"),
                })
                .Verifiable();
            var http = new HttpClient(mockHandler.Object);

            client = new APIClient(http, memCache.Object);
            var testresponse = await client.GetAsync<TestItem>(testUrl);

            Assert.IsNull(testresponse);
            

        }

        [TestMethod]
        public async Task GetAsync_Returns_Item_When_Present()
        {
            TestItem testObject = new TestItem
            {
                Id = 1,
                Price = 4.25,
                Name = "Comet"
            };

            mockHandler = new Mock<HttpMessageHandler>();
            mockHandler.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
                ).ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(testObject)),
                })
                .Verifiable();

            var http = new HttpClient(mockHandler.Object);

            client = new APIClient(http, memCache.Object);
            var testResponse = await client.GetAsync<TestItem>(testUrl);

            Assert.IsNotNull(testResponse);
            Assert.AreEqual(testResponse.Id, 1);
            Assert.AreEqual(testResponse.Price, 4.25);

        }

        [TestMethod]
        public async Task GetCachedAsync_ReturnsNullResponse()
        {
            object expected = null;
            memCache = new Mock<IMemoryCache>();
            mockHandler = new Mock<HttpMessageHandler>();
            mockHandler.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()).ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("null"),
                })
                .Verifiable();

            memCache.Setup(x => x.CreateEntry(testUrl)).Returns(new Mock<ICacheEntry>().Object);
            var http = new HttpClient(mockHandler.Object);

            client = new APIClient(http, memCache.Object);
            var testResponse = await client.GetCachedAsync<TestItem>(testUrl, TimeSpan.MinValue);

            Assert.IsNull(testResponse);
            Assert.AreEqual(expected, testResponse);


        }

        [TestMethod]
        public async Task GetCachedAsync_ReturnsItemWhenItemPresent()
        {
            TestItem testObject = new TestItem
            {
                Id = 1,
                Price = 4.25,
                Name = "Comet"
            };

            memCache = new Mock<IMemoryCache>();
            mockHandler = new Mock<HttpMessageHandler>();
            mockHandler.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()).ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(testObject)),
                })
                .Verifiable();

            memCache.Setup(x => x.CreateEntry(testUrl)).Returns(new Mock<ICacheEntry>().Object);
            var http = new HttpClient(mockHandler.Object);

            client = new APIClient(http, memCache.Object);
            var testResponse = await client.GetCachedAsync<TestItem>(testUrl, TimeSpan.MaxValue);

            Assert.IsNotNull(testResponse);
            Assert.AreEqual(testResponse.Id, 1);
            Assert.AreEqual(testResponse.Price, 4.25);


        }
        [TestMethod]
        public async Task GetCachedAsync_CallsAPIWhenNoEntryPresent()
        {
            TestItem testObject = new TestItem
            {
                Id = 1,
                Price = 4.25,
                Name = "Comet"
            };

            memCache = new Mock<IMemoryCache>();
            mockHandler = new Mock<HttpMessageHandler>();
            mockHandler.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()).ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(testObject)),
                })
                .Verifiable();

            memCache.Setup(x => x.CreateEntry(testUrl)).Returns(new Mock<ICacheEntry>().Object);
            var http = new HttpClient(mockHandler.Object);

            client = new APIClient(http, memCache.Object);
            var testresponse = await client.GetCachedAsync<TestItem>(testUrl, TimeSpan.MaxValue);

            mockHandler.Verify();
        }
        [TestMethod]
        public async Task GetCachedAsync_Returns_Cached_Entry()
        {
      

            var cachedResponse = (object)"{'id': 1, 'price': 4.25, 'name' : 'comet'}";



            memCache = new Mock<IMemoryCache>();
            mockHandler = new Mock<HttpMessageHandler>();

            memCache.Setup(x => x.TryGetValue(It.IsAny<object>(), out cachedResponse)).Returns(true);
            var http = new HttpClient(mockHandler.Object);

            client = new APIClient(http, memCache.Object);
            var testResponse = await client.GetCachedAsync<TestItem>(testUrl, TimeSpan.MaxValue);

            mockHandler.Protected().Verify<Task<HttpResponseMessage>>(
                "SendAsync",
                Times.Never(),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());

            Assert.IsNotNull(testResponse);
            Assert.AreEqual(testResponse.Id, 1);
            Assert.AreEqual(testResponse.Price, 4.25);
        }
    }



}


