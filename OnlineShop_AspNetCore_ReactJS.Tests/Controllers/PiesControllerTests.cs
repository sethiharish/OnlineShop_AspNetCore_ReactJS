using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineShop_AspNetCore_ReactJS.Controllers;
using OnlineShop_AspNetCore_ReactJS.Helpers;
using OnlineShop_AspNetCore_ReactJS.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Entities = OnlineShop_AspNetCore_ReactJS.Data.Entities;

namespace OnlineShop_AspNetCore_ReactJS.Tests.Controllers
{
    public class PiesControllerTests
    {
        private readonly Mock<IPieService> mockPieService;
        private readonly Mock<IMapper> mockMapper;
        private readonly PiesController sut;
        private readonly IEnumerable<Entities.Pie> pies;

        public PiesControllerTests()
        {
            // Arrange
            mockPieService = new Mock<IPieService>();
            mockMapper = new Mock<IMapper>();
            sut = new PiesController(mockPieService.Object, mockMapper.Object);

            var category1 = new Entities.Category { Id = 1, Name = "Category1", Description = "Category1_Description" };
            var category2 = new Entities.Category { Id = 2, Name = "Category2", Description = "Category2_Description" };
            var category3 = new Entities.Category { Id = 3, Name = "Category3", Description = "Category3_Description" };

            pies = new Entities.Pie[]
            {
                new Entities.Pie { Id = 1, Name = "Pie1", ShortDescription = "Pie1_ShortDescription", LongDescription = "Pie1_LongDescription", Price = 10.99m, IsPieOfTheWeek = true, InStock = true, ImageUrl = "Pie1_ImageUrl", ThumbnailImageUrl = "Pie1_ThumbnailImageUrl", Category = category1 },
                new Entities.Pie { Id = 2, Name = "Pie2", ShortDescription = "Pie2_ShortDescription", LongDescription = "Pie2_LongDescription", Price = 11.50m, IsPieOfTheWeek = false, InStock = false, ImageUrl = "Pie2_ImageUrl", ThumbnailImageUrl = "Pie2_ThumbnailImageUrl", Category = category2 },
                new Entities.Pie { Id = 3, Name = "Pie3", ShortDescription = "Pie3_ShortDescription", LongDescription = "Pie3_LongDescription", Price = 12.59m, IsPieOfTheWeek = false, InStock = true, ImageUrl = "Pie3_ImageUrl", ThumbnailImageUrl = "Pie3_ThumbnailImageUrl", Category = category3 },
                new Entities.Pie { Id = 4, Name = "Pie4", ShortDescription = "Pie4_ShortDescription", LongDescription = "Pie4_LongDescription", Price = 15.95m, IsPieOfTheWeek = false, InStock = true, ImageUrl = "Pie4_ImageUrl", ThumbnailImageUrl = "Pie4_ThumbnailImageUrl", Category = category1 },
                new Entities.Pie { Id = 5, Name = "Pie5", ShortDescription = "Pie5_ShortDescription", LongDescription = "Pie5_LongDescription", Price = 16.95m, IsPieOfTheWeek = true, InStock = true, ImageUrl = "Pie5_ImageUrl", ThumbnailImageUrl = "Pie5_ThumbnailImageUrl", Category = category2 },
                new Entities.Pie { Id = 6, Name = "Pie6", ShortDescription = "Pie6_ShortDescription", LongDescription = "Pie6_LongDescription", Price = 21.40m, IsPieOfTheWeek = false, InStock = false, ImageUrl = "Pie6_ImageUrl", ThumbnailImageUrl = "Pie6_ThumbnailImageUrl", Category = category3 }
            };
        }

        private IEnumerable<Entities.Pie> SeedData(bool isPieOfTheWeek = false)
        {
            if (isPieOfTheWeek)
            {
                return pies.Where(p => p.IsPieOfTheWeek).ToArray();
            }

            return pies;
        }

        private Entities.Pie SeedData(int pieId)
        {
            return pies.FirstOrDefault(p => p.Id == pieId);
        }

        private Models.Pie MapData(Entities.Pie pie)
        {
            return new Models.Pie
            {
                Id = pie.Id,
                Name = pie.Name,
                ShortDescription = pie.ShortDescription,
                LongDescription = pie.LongDescription,
                Price = pie.Price,
                IsPieOfTheWeek = pie.IsPieOfTheWeek,
                InStock = pie.InStock,
                ImageUrl = pie.ImageUrl,
                ThumbnailImageUrl = pie.ThumbnailImageUrl,
                CategoryId = pie.Category.Id,
                CategoryName = pie.Category.Name
            };
        }

        private IEnumerable<Models.Pie> MapData(IEnumerable<Entities.Pie> pies)
        {
            return pies.Select(p => MapData(p)).ToArray();
        }

        [Fact]
        public async Task GetPiesAsync_WhenCalledWithNoIsPieOfTheWeekParameter_ReturnsAllPies()
        {
            // Arrange
            var seedData = SeedData();
            var mappedData = MapData(seedData);

            mockPieService.Setup(m => m.GetPiesAsync()).ReturnsAsync(seedData);
            mockMapper.Setup(m => m.Map<Models.Pie[]>(seedData)).Returns(mappedData.ToArray());

            // Act
            var result = await sut.GetPiesAsync(null);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<IEnumerable<Models.Pie>>>(result);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Models.Pie>>(okObjectResult.Value);
            Assert.Equal(6, model.Count());

            mockPieService.Verify(m => m.GetPiesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetPiesAsync_WhenCalledWithIsPieOfTheWeekParameter_ReturnsPiesOfTheWeek()
        {
            // Arrange
            var seedData = SeedData(isPieOfTheWeek: true);
            var mappedData = MapData(seedData);

            mockPieService.Setup(m => m.GetPiesOfTheWeekAsync()).ReturnsAsync(seedData);
            mockMapper.Setup(m => m.Map<Models.Pie[]>(seedData)).Returns(mappedData.ToArray());

            // Act
            var result = await sut.GetPiesAsync(isPieOfTheWeek: true);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<IEnumerable<Models.Pie>>>(result);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Models.Pie>>(okObjectResult.Value);
            Assert.Equal(2, model.Count());

            mockPieService.Verify(m => m.GetPiesOfTheWeekAsync(), Times.Once);
        }

        [Fact]
        public async Task GetPieAsync_WhenCalledWithValidId_ReturnsPieWithId()
        {
            // Arrange
            int pieId = 1;
            var seedData = SeedData(pieId);
            var mappedData = MapData(seedData);

            mockPieService.Setup(m => m.GetPieAsync(pieId)).ReturnsAsync(seedData);
            mockMapper.Setup(m => m.Map<Models.Pie>(seedData)).Returns(mappedData);

            // Act
            var result = await sut.GetPieAsync(pieId);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<Models.Pie>>(result);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<Models.Pie>(okObjectResult.Value);
            Assert.NotNull(model);
            Assert.Equal($"Pie{pieId}", model.Name);

            mockPieService.Verify(m => m.GetPieAsync(pieId), Times.Once);
        }

        [Fact]
        public async Task GetPieAsync_WhenCalledWithInvalidId_ReturnsNotFoundError()
        {
            // Arrange
            var pieId = 7;
            var seedData = default(Entities.Pie);
            var mappedData = default(Models.Pie);

            mockPieService.Setup(m => m.GetPieAsync(pieId)).ReturnsAsync(seedData);
            mockMapper.Setup(m => m.Map<Models.Pie>(seedData)).Returns(mappedData);

            // Act
            var result = await sut.GetPieAsync(pieId);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<Models.Pie>>(result);
            var notFoundObjectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal(ErrorMessage.InvalidData(Constant.NotFound, typeof(Models.Pie), Constant.Id, pieId.ToString()), notFoundObjectResult.Value);

            mockPieService.Verify(m => m.GetPieAsync(pieId), Times.Once);
        }
    }
}
