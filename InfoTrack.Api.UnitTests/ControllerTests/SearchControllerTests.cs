﻿using InfoTrack.Api.Controllers;
using InfoTrack.Api.Dtos;
using InfoTrack.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InfoTrack.Api.UnitTests.SearchControllerTests
{

    [TestClass]
    public class SearchControllerTests
    {
        private readonly SearchController _controller;
        private readonly Mock<ISearchService> _mockSearchService;
        private SearchDto _dto;

        public SearchControllerTests()
        {
            _mockSearchService = new Mock<ISearchService>();
            _controller = new SearchController(_mockSearchService.Object);
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
        public async Task GetSearchResult_ShouldReturn_ExpectedResult()
        {
            // Arrange
            var expectedResult = new SearchResultDto
            {
                Url = "Test",
                SearchTerm = "Test",
                Ranking = "1, 5, 7",
                SearchDate = new DateTime(2024, 05, 12)
            };

            _mockSearchService.Setup(x => x.Search(It.IsAny<SearchDto>()))
                .ReturnsAsync(expectedResult);

            // Act
            var actualResult = await _controller.GetSearchResult(_dto);
            var okResult = actualResult as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
        }
    }
}
