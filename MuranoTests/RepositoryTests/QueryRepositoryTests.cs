using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApplication10.DataAccessLayer;
using WebApplication10.DataAccessLayer.Models;
using WebApplication10.DataAccessLayer.Repository.Implementations;

namespace MuranoTests.RepositoryTests
{
    public class QueryRepositoryTests
    {
        private readonly DbContextOptions<SearchContext> _options;

        public QueryRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<SearchContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }

        [Fact]
        public async Task InsertQueryAsync_Should_InsertQueries_And_ReturnCount()
        {
            // Arrange
            using (var context = new SearchContext(_options))
            {
                var repository = new QueryRepository(context);
                var queries = new List<Query>
                {
                    new Query { QueryKeywordHash = "hash1" },
                    new Query { QueryKeywordHash = "hash2" }
                };

                // Act
                var result = await repository.InsertQueryAsync(queries);

                // Assert
                result.Should().Be(2); 
            }
        }

        [Fact]
        public async Task GetQueryByHashAsync_Should_ReturnQuery_When_HashExists()
        {
            // Arrange
            using (var context = new SearchContext(_options))
            {
                var repository = new QueryRepository(context);
                var query = new Query { QueryKeywordHash = "hash1" };
                context.Queries.Add(query);
                await context.SaveChangesAsync();

                // Act
                var result = await repository.GetQueryByHashAsync("hash1");

                // Assert
                result.Should().NotBeNull();
                result?.QueryKeywordHash.Should().Be("hash1");
            }
        }

        [Fact]
        public async Task GetQueryByHashAsync_Should_ReturnNull_When_HashDoesNotExist()
        {
            // Arrange
            using (var context = new SearchContext(_options))
            {
                var repository = new QueryRepository(context);
                var query = new Query { QueryKeywordHash = "hash1" };
                context.Queries.Add(query);
                await context.SaveChangesAsync();

                // Act
                var result = await repository.GetQueryByHashAsync("nonexistent_hash");

                // Assert
                result.Should().BeNull();
            }
        }
    }
}
