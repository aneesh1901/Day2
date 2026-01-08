using Day2;
using Xunit;

namespace Day2.Tests
{
    public class ProductTests
    {
        [Fact]
        public void IsValid_WithValidNameAndPrice_ReturnsTrue()
        {
            // Arrange
            var product = new Product
            {
                Name = "Laptop",
                Price = 1000
            };

            // Act
            var result = product.IsValid();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValid_WithEmptyName_ReturnsFalse()
        {
            // Arrange
            var product = new Product
            {
                Name = "   ",   // only spaces
                Price = 1000
            };

            // Act
            var result = product.IsValid();

            // Assert
            Assert.False(result);
        }

    }
}
