using OnlineShop_AspNetCore_ReactJS.Data;
using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using OnlineShop_AspNetCore_ReactJS.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShop_AspNetCore_ReactJS.Tests.Services
{
    public class BannerServiceTests : ServiceTestsBase
    {
        public BannerServiceTests()
        {
            // Arrange
            var banner1 = new Banner { Id = 1, Name = "Banner1", Description = "Banner1_Description", ImageUrl = "Banner1_ImageUrl" };
            var banner2 = new Banner { Id = 2, Name = "Banner2", Description = "Banner2_Description", ImageUrl = "Banner2_ImageUrl" };
            var banner3 = new Banner { Id = 3, Name = "Banner3", Description = "Banner3_Description", ImageUrl = "Banner3_ImageUrl" };

            using (var context = new OnlineShopContext(options))
            {
                context.Banner.AddRange(banner1, banner2, banner3);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetBannersAsync_WhenCalled_ReturnsAllBanners()
        {
            // Act
            IEnumerable<Banner> banners = null;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new BannerService(context);
                banners = await sut.GetBannersAsync();
            }

            // Assert
            Assert.NotNull(banners);
            Assert.Equal(3, banners.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetBannerAsync_WhenCalledWithValidId_ReturnsBannerWithId(int bannerId)
        {
            // Act
            Banner banner = null;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new BannerService(context);
                banner = await sut.GetBannerAsync(bannerId);
            }

            // Assert
            Assert.NotNull(banner);
            Assert.Equal($"Banner{bannerId}", banner.Name);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public async Task GetBannerAsync_WhenCalledWithInvalidId_ReturnsNull(int bannerId)
        {
            // Act
            Banner banner = null;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new BannerService(context);
                banner = await sut.GetBannerAsync(bannerId);
            }

            // Assert
            Assert.Null(banner);
        }
    }
}
