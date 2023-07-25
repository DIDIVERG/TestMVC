using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApplication10.DataAccessLayer;
using WebApplication10.DataAccessLayer.Models;
using WebApplication10.DataAccessLayer.Repository.Implementations;

namespace MuranoTests.RepositoryTests
{
    public class SearchResultRepositoryTests
    {
        private readonly DbContextOptions<SearchContext> _options;

        public SearchResultRepositoryTests()
        {
            // Set up an in-memory database for testing
            _options = new DbContextOptionsBuilder<SearchContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }

        [Fact]
        public async Task InsertResultAsync_Should_InsertResults_And_ReturnCount()
        {
            // Arrange
            using (var context = new SearchContext(_options))
            {
                var repository = new SearchResultRepository(context);
                var results = new List<SearchResult>
                {
                    new SearchResult { Url = "https://example.com", Snippet = "Snippet 1" },
                    new SearchResult { Url = "https://example.org", Snippet = "Snippet 2" }
                };

                // Act
                var result = await repository.InsertResultAsync(results);

                // Assert
                result.Should().Be(2); 
            }
        }
        
    }
}
