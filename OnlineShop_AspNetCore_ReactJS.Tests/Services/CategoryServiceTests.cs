using OnlineShop_AspNetCore_ReactJS.Data;
using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using OnlineShop_AspNetCore_ReactJS.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShop_AspNetCore_ReactJS.Tests.Services
{
    public class CategoryServiceTests : ServiceTestsBase
    {
        public CategoryServiceTests()
        {
            // Arrange
            var category1 = new Category { Id = 1, Name = "Category1", Description = "Category1_Description" };
            var category2 = new Category { Id = 2, Name = "Category2", Description = "Category2_Description" };
            var category3 = new Category { Id = 3, Name = "Category3", Description = "Category3_Description" };

            using (var context = new OnlineShopContext(options))
            {
                context.Category.AddRange(category1, category2, category3);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetCategoriesAsync_WhenCalled_ReturnsAllCategories()
        {
            // Act
            IEnumerable<Category> categories = null;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new CategoryService(context);
                categories = await sut.GetCategoriesAsync();
            }

            // Assert
            Assert.NotNull(categories);
            Assert.Equal(3, categories.Count());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetCategoryAsync_WhenCalledWithValidId_ReturnsCategoryWithId(int categoryId)
        {
            // Act
            Category category = null;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new CategoryService(context);
                category = await sut.GetCategoryAsync(categoryId);
            }

            // Assert
            Assert.NotNull(category);
            Assert.Equal($"Category{categoryId}", category.Name);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public async Task GetCategoryAsync_WhenCalledWithInvalidId_ReturnsNull(int categoryId)
        {
            // Act
            Category category = null;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new CategoryService(context);
                category = await sut.GetCategoryAsync(categoryId);
            }

            // Assert
            Assert.Null(category);
        }
    }
}
