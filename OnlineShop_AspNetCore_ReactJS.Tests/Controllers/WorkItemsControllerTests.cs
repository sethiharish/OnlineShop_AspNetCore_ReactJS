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
    public class WorkItemsControllerTests
    {
        private readonly Mock<IWorkItemService> mockWorkItemService;
        private readonly Mock<IMapper> mockMapper;
        private readonly WorkItemsController sut;
        private readonly IEnumerable<Entities.WorkItem> workItems;

        public WorkItemsControllerTests()
        {
            // Arrange
            mockWorkItemService = new Mock<IWorkItemService>();
            mockMapper = new Mock<IMapper>();
            sut = new WorkItemsController(mockWorkItemService.Object, mockMapper.Object);

            var iteration1 = new Entities.Iteration { Id = 1, Name = "Iteration1" };
            var iteration2 = new Entities.Iteration { Id = 2, Name = "Iteration2" };
            var iteration3 = new Entities.Iteration { Id = 3, Name = "Iteration3" };

            workItems = new Entities.WorkItem[]
            {
                new Entities.WorkItem { Id = 1, Name = "Iteration1", ImageUrl = "Iteration1_ImageUrl", Iteration = iteration1 },
                new Entities.WorkItem { Id = 2, Name = "Iteration2", ImageUrl = "Iteration2_ImageUrl", Iteration = iteration2 },
                new Entities.WorkItem { Id = 3, Name = "Iteration3", ImageUrl = "Iteration3_ImageUrl", Iteration = iteration3 }
            };
        }

        private IEnumerable<Entities.WorkItem> SeedData()
        {
            return workItems;
        }
        
        private Models.WorkItem MapData(Entities.WorkItem workItem)
        {
            return new Models.WorkItem
            {
                Id = workItem.Id,
                Name = workItem.Name,
                ImageUrl = workItem.ImageUrl,
                IterationId = workItem.Iteration.Id,
                IterationName = workItem.Iteration.Name
            };
        }

        private IEnumerable<Models.WorkItem> MapData(IEnumerable<Entities.WorkItem> workItems)
        {
            return workItems.Select(w => MapData(w)).ToArray();
        }

        [Fact]
        public async Task GetWorkItemsAsync_WhenCalled_ReturnsAllWorkItems()
        {
            // Arrange
            var seedData = SeedData();
            var mappedData = MapData(seedData);

            mockWorkItemService.Setup(m => m.GetWorkItemsAsync()).ReturnsAsync(seedData);
            mockMapper.Setup(m => m.Map<Models.WorkItem[]>(seedData)).Returns(mappedData.ToArray());

            // Act
            var result = await sut.GetWorkItemsAsync();

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<IEnumerable<Models.WorkItem>>>(result);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Models.WorkItem>>(okObjectResult.Value);
            Assert.Equal(3, model.Count());

            mockWorkItemService.Verify(m => m.GetWorkItemsAsync(), Times.Once);
        }
    }
}
