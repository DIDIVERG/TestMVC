using FluentAssertions;
using WebApplication10.Services.Implementations;

namespace MuranoTests.ServicesTests
{
    public class HashServiceTests
    {
        [Fact]
        public void GetHash_Should_Return_Same_Hash_For_Same_Keywords()
        {
            // Arrange
            var hashService = new HashService();
            var keywords = new List<string> { "apple", "orange", "banana" };

            // Act
            var hash1 = hashService.GetHash(keywords);
            var hash2 = hashService.GetHash(keywords);

            // Assert
            hash1.Should().Be(hash2);
        }

        [Fact]
        public void GetHash_Should_Return_Same_Hash_For_Same_Keywords_Ignoring_Case()
        {
            // Arrange
            var hashService = new HashService();
            var keywords1 = new List<string> { "Apple", "Orange", "Banana" };
            var keywords2 = new List<string> { "apple", "orange", "banana" };

            // Act
            var hash1 = hashService.GetHash(keywords1);
            var hash2 = hashService.GetHash(keywords2);

            // Assert
            hash1.Should().Be(hash2);
        }

        [Fact]
        public void GetHash_Should_Return_Different_Hash_For_Different_Combined_Keywords()
        {
            // Arrange
            var hashService = new HashService();
            var keywords1 = new List<string> { "apple", "orange", "banana" };
            var keywords2 = new List<string> { "appleorange", "banana" };

            // Act
            var hash1 = hashService.GetHash(keywords1);
            var hash2 = hashService.GetHash(keywords2);

            // Assert
            hash1.Should().NotBe(hash2);
        }
    }
}
