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
    public class BannersControllerTests
    {
        private readonly Mock<IBannerService> mockBannerService;
        private readonly Mock<IMapper> mockMapper;
        private readonly BannersController sut;
        private readonly IEnumerable<Entities.Banner> banners;

        public BannersControllerTests()
        {
            // Arrange
            mockBannerService = new Mock<IBannerService>();
            mockMapper = new Mock<IMapper>();
            sut = new BannersController(mockBannerService.Object, mockMapper.Object);

            banners = new Entities.Banner[]
            {
                new Entities.Banner { Id = 1, Name = "Banner1", Description = "Banner1_Description", ImageUrl = "Banner1_ImageUrl" },
                new Entities.Banner { Id = 2, Name = "Banner2", Description = "Banner2_Description", ImageUrl = "Banner2_ImageUrl" },
                new Entities.Banner { Id = 3, Name = "Banner3", Description = "Banner3_Description", ImageUrl = "Banner3_ImageUrl" }
            };
        }

        private IEnumerable<Entities.Banner> SeedData()
        {
            return banners;
        }

        private Entities.Banner SeedData(int bannerId)
        {
            return banners.FirstOrDefault(b => b.Id == bannerId);
        }

        private Models.Banner MapData(Entities.Banner banner)
        {
            return new Models.Banner
            {
                Id = banner.Id,
                Name = banner.Name,
                Description = banner.Description,
                ImageUrl = banner.ImageUrl
            };
        }

        private IEnumerable<Models.Banner> MapData(IEnumerable<Entities.Banner> banners)
        {
            return banners.Select(b => MapData(b)).ToArray();
        }

        [Fact]
        public async Task GetBannersAsync_WhenCalled_ReturnsAllBanners()
        {
            // Arrange
            var seedData = SeedData();
            var mappedData = MapData(seedData);

            mockBannerService.Setup(m => m.GetBannersAsync()).ReturnsAsync(seedData);
            mockMapper.Setup(m => m.Map<Models.Banner[]>(seedData)).Returns(mappedData.ToArray());

            // Act
            var result = await sut.GetBannersAsync();

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<IEnumerable<Models.Banner>>>(result);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Models.Banner>>(okObjectResult.Value);
            Assert.Equal(3, model.Count());

            mockBannerService.Verify(m => m.GetBannersAsync(), Times.Once);
        }

        [Fact]
        public async Task GetBannerAsync_WhenCalledWithValidId_ReturnsBannerWithId()
        {
            // Arrange
            int bannerId = 1;
            var seedData = SeedData(bannerId);
            var mappedData = MapData(seedData);

            mockBannerService.Setup(m => m.GetBannerAsync(bannerId)).ReturnsAsync(seedData);
            mockMapper.Setup(m => m.Map<Models.Banner>(seedData)).Returns(mappedData);

            // Act
            var result = await sut.GetBannerAsync(bannerId);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<Models.Banner>>(result);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<Models.Banner>(okObjectResult.Value);
            Assert.NotNull(model);
            Assert.Equal($"Banner{bannerId}", model.Name);

            mockBannerService.Verify(m => m.GetBannerAsync(bannerId), Times.Once);
        }

        [Fact]
        public async Task GetBannerAsync_WhenCalledWithInvalidId_ReturnsNotFoundError()
        {
            // Arrange
            var bannerId = 4;
            var seedData = default(Entities.Banner);
            var mappedData = default(Models.Banner);

            mockBannerService.Setup(m => m.GetBannerAsync(bannerId)).ReturnsAsync(seedData);
            mockMapper.Setup(m => m.Map<Models.Banner>(seedData)).Returns(mappedData);

            // Act
            var result = await sut.GetBannerAsync(bannerId);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<Models.Banner>>(result);
            var notFoundObjectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal(ErrorMessage.InvalidData(Constant.NotFound, typeof(Models.Banner), Constant.Id, bannerId.ToString()), notFoundObjectResult.Value);

            mockBannerService.Verify(m => m.GetBannerAsync(bannerId), Times.Once);
        }
    }
}
