using OnlineShop_AspNetCore_ReactJS.Data;
using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using OnlineShop_AspNetCore_ReactJS.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShop_AspNetCore_ReactJS.Tests.Services
{
    public class WorkItemServiceTests : ServiceTestsBase
    {
        public WorkItemServiceTests()
        {
            // Arrange
            var iteration1 = new Iteration { Id = 1, Name = "Iteration1" };
            var iteration2 = new Iteration { Id = 2, Name = "Iteration2" };
            var iteration3 = new Iteration { Id = 3, Name = "Iteration3" };

            var workItem1 = new WorkItem { Id = 1, Name = "Iteration1", ImageUrl = "Iteration1_ImageUrl", Iteration = iteration1 };
            var workItem2 = new WorkItem { Id = 2, Name = "Iteration2", ImageUrl = "Iteration2_ImageUrl", Iteration = iteration2 };
            var workItem3 = new WorkItem { Id = 3, Name = "Iteration3", ImageUrl = "Iteration3_ImageUrl", Iteration = iteration3 };

            using (var context = new OnlineShopContext(options))
            {
                context.Iteration.AddRange(iteration1, iteration2, iteration3);
                context.WorkItem.AddRange(workItem1, workItem2, workItem3);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetWorkItemsAsync_WhenCalled_ReturnsAllWorkItemsWithIteration()
        {
            // Act
            List<WorkItem> workItems = null;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new WorkItemService(context);
                workItems = (await sut.GetWorkItemsAsync()).ToList();
            }

            // Assert
            Assert.NotNull(workItems);
            Assert.Equal(3, workItems.Count());
            Assert.NotNull(workItems[0].Iteration);
            Assert.NotNull(workItems[1].Iteration);
            Assert.NotNull(workItems[2].Iteration);
        }
    }
}
