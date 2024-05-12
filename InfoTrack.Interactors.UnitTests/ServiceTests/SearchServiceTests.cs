using InfoTrack.Api.Dtos;
using InfoTrack.Data.Models;
using InfoTrack.Interactors.Interfaces;
using InfoTrack.Interactors.Services;
using InfoTract.Helpers.ExceptionHandler;
using Moq;
using Moq.Protected;
using System.Net;

namespace InfoTrack.Interactors.UnitTests.ServiceTests
{
    [TestClass]
    public class SearchServiceTests
    {
        private readonly SearchService _service;
        private readonly Mock<ISearchRepository> _mockSearchRepository;
        private readonly HttpClient _httpClient;
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private SearchDto _dto;

        public SearchServiceTests()
        {
            _mockSearchRepository = new Mock<ISearchRepository>();
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _service = new SearchService(_httpClient, _mockSearchRepository.Object);
        }

        [TestInitialize]
        public void TestInit()
        {
            _dto = new SearchDto
            {
                Url = "test.co.uk",
                SearchTerm = "search term test"
            };
        }

        [TestMethod]
        public async Task Search_ShouldCall_Repository()
        {
            // Arrange
            _mockHttpMessageHandler.Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK
                    })
                    .Verifiable();

            // Act
            var result = await _service.Search(_dto);

            // Assert
            _mockSearchRepository.Verify(x => x.AddAsync(It.IsAny<SearchResult>()), Times.Once);
        }

        [TestMethod]
        public async Task Search_ShouldCall_ReturnOneResult()
        {
            // Arrange
            var expectedRanking = "0";
            var expectedUrl = _dto.Url;

            _mockHttpMessageHandler.Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent("a href=\"/test.co.uk")
                    })
                    .Verifiable();

            // Act
            var actualResult = await _service.Search(_dto);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedRanking, actualResult.Ranking);
            Assert.AreEqual(expectedUrl, actualResult.Url);
        }

        [TestMethod]
        public async Task Search_ShouldCall_ThrowError()
        {
            // Arrange
            _mockHttpMessageHandler.Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.BadGateway
                    })
                    .Verifiable();

            // Act

            // Assert
            await Assert.ThrowsExceptionAsync<SearchException>(() => _service.Search(_dto));
            
        }

    }
}
