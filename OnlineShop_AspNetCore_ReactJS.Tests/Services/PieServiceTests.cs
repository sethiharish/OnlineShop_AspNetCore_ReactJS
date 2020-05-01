using OnlineShop_AspNetCore_ReactJS.Data;
using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using OnlineShop_AspNetCore_ReactJS.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShop_AspNetCore_ReactJS.Tests.Services
{
    public class PieServiceTests : ServiceTestsBase
    {
        public PieServiceTests()
        {
            // Arrange
            var category1 = new Category { Id = 1, Name = "Category1", Description = "Category1_Description" };
            var category2 = new Category { Id = 2, Name = "Category2", Description = "Category2_Description" };
            var category3 = new Category { Id = 3, Name = "Category3", Description = "Category3_Description" };

            var pie1 = new Pie { Id = 1, Name = "Pie1", ShortDescription = "Pie1_ShortDescription", LongDescription = "Pie1_LongDescription", Price = 10.99m, IsPieOfTheWeek = true, InStock = true, ImageUrl = "Pie1_ImageUrl", ThumbnailImageUrl = "Pie1_ThumbnailImageUrl", Category = category1 };
            var pie2 = new Pie { Id = 2, Name = "Pie2", ShortDescription = "Pie2_ShortDescription", LongDescription = "Pie2_LongDescription", Price = 11.50m, IsPieOfTheWeek = false, InStock = false, ImageUrl = "Pie2_ImageUrl", ThumbnailImageUrl = "Pie2_ThumbnailImageUrl", Category = category2 };
            var pie3 = new Pie { Id = 3, Name = "Pie3", ShortDescription = "Pie3_ShortDescription", LongDescription = "Pie3_LongDescription", Price = 12.59m, IsPieOfTheWeek = false, InStock = true, ImageUrl = "Pie3_ImageUrl", ThumbnailImageUrl = "Pie3_ThumbnailImageUrl", Category = category3 };
            var pie4 = new Pie { Id = 4, Name = "Pie4", ShortDescription = "Pie4_ShortDescription", LongDescription = "Pie4_LongDescription", Price = 15.95m, IsPieOfTheWeek = false, InStock = true, ImageUrl = "Pie4_ImageUrl", ThumbnailImageUrl = "Pie4_ThumbnailImageUrl", Category = category1 };
            var pie5 = new Pie { Id = 5, Name = "Pie5", ShortDescription = "Pie5_ShortDescription", LongDescription = "Pie5_LongDescription", Price = 16.95m, IsPieOfTheWeek = true, InStock = true, ImageUrl = "Pie5_ImageUrl", ThumbnailImageUrl = "Pie5_ThumbnailImageUrl", Category = category2 };
            var pie6 = new Pie { Id = 6, Name = "Pie6", ShortDescription = "Pie6_ShortDescription", LongDescription = "Pie6_LongDescription", Price = 21.40m, IsPieOfTheWeek = false, InStock = false, ImageUrl = "Pie6_ImageUrl", ThumbnailImageUrl = "Pie6_ThumbnailImageUrl", Category = category3 };

            using (var context = new OnlineShopContext(options))
            {
                context.Category.AddRange(category1, category2, category3);
                context.Pie.AddRange(pie1, pie2, pie3, pie4, pie5, pie6);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetPiesAsync_WhenCalled_ReturnsAllPiesWithCategory()
        {
            // Act
            List<Pie> pies = null;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new PieService(context);
                pies = (await sut.GetPiesAsync()).ToList();
            }

            // Assert
            Assert.NotNull(pies);
            Assert.Equal(6, pies.Count());
            Assert.NotNull(pies[0].Category);
            Assert.NotNull(pies[1].Category);
            Assert.NotNull(pies[2].Category);
            Assert.NotNull(pies[3].Category);
            Assert.NotNull(pies[4].Category);
            Assert.NotNull(pies[5].Category);
        }

        [Fact]
        public async Task GetPiesOfTheWeekAsync_WhenCalled_ReturnsPiesOfTheWeekWithCategory()
        {
            // Act
            List<Pie> pies = null;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new PieService(context);
                pies = (await sut.GetPiesOfTheWeekAsync()).ToList();
            }

            // Assert
            Assert.NotNull(pies);
            Assert.Equal(2, pies.Count());
            Assert.NotNull(pies[0].Category);
            Assert.NotNull(pies[1].Category);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public async Task GetPieAsync_WhenCalledWithValidId_ReturnsPieWithIdWithCategory(int pieId)
        {
            // Act
            Pie pie = null;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new PieService(context);
                pie = await sut.GetPieAsync(pieId);
            }

            // Assert
            Assert.NotNull(pie);
            Assert.Equal($"Pie{pieId}", pie.Name);
            Assert.NotNull(pie.Category);
        }

        [Theory]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        public async Task GetPieAsync_WhenCalledWithInvalidId_ReturnsNull(int pieId)
        {
            // Act
            Pie pie = null;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new PieService(context);
                pie = await sut.GetPieAsync(pieId);
            }

            // Assert
            Assert.Null(pie);
        }
    }
}
