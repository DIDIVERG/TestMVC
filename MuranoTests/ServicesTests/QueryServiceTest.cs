using FluentAssertions;
using Moq;
using WebApplication10.DataAccessLayer.Models;
using WebApplication10.DataAccessLayer.Repository.Interfaces;
using WebApplication10.Services.Implementations;

namespace MuranoTests.ServicesTests
{
    public class QueryServiceTests
    {
        

        [Fact]
        public async Task GetQueryByHashAsync_Should_ReturnQuery_When_HashExists()
        {
            // Arrange
            var mockQueryRepository = new Mock<IQueryRepository>();
            var queryService = new QueryService(mockQueryRepository.Object);
            var hash = "existing_hash";
            var query = new Query { Id = 1, QueryKeywordHash = hash };
            mockQueryRepository.Setup(repo => repo.GetQueryByHashAsync(hash)).ReturnsAsync(query);

            // Act
            var result = await queryService.GetQueryByHashAsync(hash);

            // Assert
            result.Should().NotBeNull();
            result?.QueryKeywordHash.Should().Be(hash);
            mockQueryRepository.Verify(repo => repo.GetQueryByHashAsync(hash), Times.Once); 
        }

        [Fact]
        public async Task GetQueryByHashAsync_Should_ReturnNull_When_HashDoesNotExist()
        {
            // Arrange
            var mockQueryRepository = new Mock<IQueryRepository>();
            var queryService = new QueryService(mockQueryRepository.Object);
            var hash = "nonexistent_hash";
            mockQueryRepository.Setup(repo => repo.GetQueryByHashAsync(hash)).ReturnsAsync((Query)null);

            // Act
            var result = await queryService.GetQueryByHashAsync(hash);

            // Assert
            result.Should().BeNull();
            mockQueryRepository.Verify(repo => repo.GetQueryByHashAsync(hash), Times.Once); 
        }

        [Fact]
        public async Task CheckExistenceAsync_Should_Call_Repository_Method()
        {
            // Arrange
            var mockQueryRepository = new Mock<IQueryRepository>();
            var queryService = new QueryService(mockQueryRepository.Object);
            var query = new Query { Id = 1, QueryKeywordHash = "hash1" };

            // Act
            var result = await queryService.CheckExistenceAsync(query);

            // Assert
            result.Should().BeFalse(); 
            mockQueryRepository.Verify(repo => repo.CheckExistenceAsync(query), Times.Once); 
        }
        
    }
}
