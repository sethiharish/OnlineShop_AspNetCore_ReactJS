using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineShop_AspNetCore_ReactJS.Controllers;
using OnlineShop_AspNetCore_ReactJS.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Entities = OnlineShop_AspNetCore_ReactJS.Data.Entities;

namespace OnlineShop_AspNetCore_ReactJS.Tests.Controllers
{
    public class IterationsControllerTests
    {
        private readonly Mock<IIterationService> mockIterationService;
        private readonly Mock<IMapper> mockMapper;
        private readonly IterationsController sut;
        private readonly IEnumerable<Entities.Iteration> iterations;

        public IterationsControllerTests()
        {
            // Arrange
            mockIterationService = new Mock<IIterationService>();
            mockMapper = new Mock<IMapper>();
            sut = new IterationsController(mockIterationService.Object, mockMapper.Object);

            iterations = new Entities.Iteration[]
            {
                new Entities.Iteration { Id = 1, Name = "Iteration1" },
                new Entities.Iteration { Id = 2, Name = "Iteration2" },
                new Entities.Iteration { Id = 3, Name = "Iteration3" }
            };
        }

        private IEnumerable<Entities.Iteration> SeedData()
        {
            return iterations;
        }

        private Models.Iteration MapData(Entities.Iteration iteration)
        {
            return new Models.Iteration
            {
                Id = iteration.Id,
                Name = iteration.Name
            };
        }

        private IEnumerable<Models.Iteration> MapData(IEnumerable<Entities.Iteration> iterations)
        {
            return iterations.Select(i => MapData(i)).ToArray();
        }

        [Fact]
        public async Task GetIterationsAsync_WhenCalled_ReturnsAllIterations()
        {
            // Arrange
            var seedData = SeedData();
            var mappedData = MapData(seedData);

            mockIterationService.Setup(m => m.GetIterationsAsync()).ReturnsAsync(seedData);
            mockMapper.Setup(m => m.Map<Models.Iteration[]>(seedData)).Returns(mappedData.ToArray());

            // Act
            var result = await sut.GetIterationsAsync();

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<IEnumerable<Models.Iteration>>>(result);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Models.Iteration>>(okObjectResult.Value);
            Assert.Equal(3, model.Count());

            mockIterationService.Verify(m => m.GetIterationsAsync(), Times.Once);
        }
    }
}
