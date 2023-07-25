using FluentAssertions;
using Moq;
using WebApplication10.DataAccessLayer.Models;
using WebApplication10.DataAccessLayer.Repository.Interfaces;
using WebApplication10.Services.Implementations;

namespace MuranoTests.ServicesTests
{
    public class SearchResultServiceTests
    {
        [Fact]
        public async Task InsertResultAsync_Should_InsertNewResults_And_ReturnCount()
        {
            // Arrange
            var mockResultRepository = new Mock<ISearchResultRepository>();
            var searchResultService = new SearchResultService(mockResultRepository.Object);
            var existingResult = new SearchResult { Id = 1, Url = "https://example.com", Snippet = "Snippet 1" };
            var newResult1 = new SearchResult { Url = "https://example.org", Snippet = "Snippet 2" };
            var newResult2 = new SearchResult { Url = "https://example.net", Snippet = "Snippet 3" };
            var results = new List<SearchResult> { existingResult, newResult1, newResult2 };

            
            mockResultRepository.Setup(repo => repo.CheckExistenceAsync(existingResult)).ReturnsAsync(true);
            mockResultRepository.Setup(repo => repo.CheckExistenceAsync(newResult1)).ReturnsAsync(false);
            mockResultRepository.Setup(repo => repo.CheckExistenceAsync(newResult2)).ReturnsAsync(false);

            // Act
            var result = await searchResultService.InsertResultAsync(results);

            // Assert
            result.Should().Be(2); 
            mockResultRepository.Verify(repo => repo.InsertResultAsync(It.IsAny<IEnumerable<SearchResult>>()), Times.Once); // Verify that the repository method was called once
            mockResultRepository.Verify(repo => repo.CheckExistenceAsync(existingResult), Times.Once); // Verify that the repository method was called once for the existing result
            mockResultRepository.Verify(repo => repo.CheckExistenceAsync(newResult1), Times.Once); // Verify that the repository method was called once for the new result 1
            mockResultRepository.Verify(repo => repo.CheckExistenceAsync(newResult2), Times.Once); // Verify that the repository method was called once for the new result 2
        }

        [Fact]
        public async Task InsertResultAsync_Should_InsertAllResults_When_AllAreNew()
        {
            // Arrange
            var mockResultRepository = new Mock<ISearchResultRepository>();
            var searchResultService = new SearchResultService(mockResultRepository.Object);
            var newResult1 = new SearchResult { Url = "https://example.org", Snippet = "Snippet 1" };
            var newResult2 = new SearchResult { Url = "https://example.net", Snippet = "Snippet 2" };
            var results = new List<SearchResult> { newResult1, newResult2 };
            
            mockResultRepository.Setup(repo => repo.CheckExistenceAsync(It.IsAny<SearchResult>())).ReturnsAsync(false);

            // Act
            var result = await searchResultService.InsertResultAsync(results);

            // Assert
            result.Should().Be(2); 
            mockResultRepository.Verify(repo => repo.InsertResultAsync(results), Times.Once); 
            mockResultRepository.Verify(repo => repo.CheckExistenceAsync(It.IsAny<SearchResult>()), Times.Exactly(2)); 
        }
        
    }
}
