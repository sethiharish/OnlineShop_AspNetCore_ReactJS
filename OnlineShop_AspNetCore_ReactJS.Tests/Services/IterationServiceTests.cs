using OnlineShop_AspNetCore_ReactJS.Data;
using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using OnlineShop_AspNetCore_ReactJS.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShop_AspNetCore_ReactJS.Tests.Services
{
    public class IterationServiceTests : ServiceTestsBase
    {
        public IterationServiceTests()
        {
            // Arrange
            var iteration1 = new Iteration { Id = 1, Name = "Iteration1" };
            var iteration2 = new Iteration { Id = 2, Name = "Iteration2" };
            var iteration3 = new Iteration { Id = 3, Name = "Iteration3" };

            using (var context = new OnlineShopContext(options))
            {
                context.Iteration.AddRange(iteration1, iteration2, iteration3);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetIterationsAsync_WhenCalled_ReturnsAllIterations()
        {
            // Act
            IEnumerable<Iteration> iterations = null;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new IterationService(context);
                iterations = await sut.GetIterationsAsync();
            }

            // Assert
            Assert.NotNull(iterations);
            Assert.Equal(3, iterations.Count());
        }
    }
}
