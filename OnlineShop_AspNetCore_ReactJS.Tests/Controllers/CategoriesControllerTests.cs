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
    public class CategoriesControllerTests
    {
        private readonly Mock<ICategoryService> mockCategoryService;
        private readonly Mock<IMapper> mockMapper;
        private readonly CategoriesController sut;
        private readonly IEnumerable<Entities.Category> categories;

        public CategoriesControllerTests()
        {
            // Arrange
            mockCategoryService = new Mock<ICategoryService>();
            mockMapper = new Mock<IMapper>();
            sut = new CategoriesController(mockCategoryService.Object, mockMapper.Object);

            categories = new Entities.Category[]
            {
                new Entities.Category { Id = 1, Name = "Category1", Description = "Category1_Description" },
                new Entities.Category { Id = 2, Name = "Category2", Description = "Category2_Description" },
                new Entities.Category { Id = 3, Name = "Category3", Description = "Category3_Description" }
            };
        }

        private IEnumerable<Entities.Category> SeedData()
        {
            return categories;
        }

        private Entities.Category SeedData(int categoryId)
        {
            return categories.FirstOrDefault(c => c.Id == categoryId);
        }

        private Models.Category MapData(Entities.Category category)
        {
            return new Models.Category
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        private IEnumerable<Models.Category> MapData(IEnumerable<Entities.Category> categories)
        {
            return categories.Select(c => MapData(c)).ToArray();
        }

        [Fact]
        public async Task GetCategoriesAsync__WhenCalled_ReturnsAllCategories()
        {
            // Arrange
            var seedData = SeedData();
            var mappedData = MapData(seedData);

            mockCategoryService.Setup(m => m.GetCategoriesAsync()).ReturnsAsync(seedData);
            mockMapper.Setup(m => m.Map<Models.Category[]>(seedData)).Returns(mappedData.ToArray());

            // Act
            var result = await sut.GetCategoriesAsync();

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<IEnumerable<Models.Category>>>(result);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Models.Category>>(okObjectResult.Value);
            Assert.Equal(3, model.Count());

            mockCategoryService.Verify(m => m.GetCategoriesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetCategoryAsync_WhenCalledWithValidId_ReturnsCategoryWithId()
        {
            // Arrange
            int categoryId = 1;
            var seedData = SeedData(categoryId);
            var mappedData = MapData(seedData);

            mockCategoryService.Setup(m => m.GetCategoryAsync(categoryId)).ReturnsAsync(seedData);
            mockMapper.Setup(m => m.Map<Models.Category>(seedData)).Returns(mappedData);

            // Act
            var result = await sut.GetCategoryAsync(categoryId);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<Models.Category>>(result);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<Models.Category>(okObjectResult.Value);
            Assert.NotNull(model);
            Assert.Equal($"Category{categoryId}", model.Name);

            mockCategoryService.Verify(m => m.GetCategoryAsync(categoryId), Times.Once);
        }

        [Fact]
        public async Task GetCategoryAsync_WhenCalledWithInvalidId_ReturnsNotFoundError()
        {
            // Arrange
            var categoryId = 4;
            var seedData = default(Entities.Category);
            var mappedData = default(Models.Category);

            mockCategoryService.Setup(m => m.GetCategoryAsync(categoryId)).ReturnsAsync(seedData);
            mockMapper.Setup(m => m.Map<Models.Category>(seedData)).Returns(mappedData);

            // Act
            var result = await sut.GetCategoryAsync(categoryId);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<Models.Category>>(result);
            var notFoundObjectResult = Assert.IsAssignableFrom<NotFoundObjectResult>(actionResult.Result);
            Assert.Equal(ErrorMessage.InvalidData(Constant.NotFound, typeof(Models.Category), Constant.Id, categoryId.ToString()), notFoundObjectResult.Value);

            mockCategoryService.Verify(m => m.GetCategoryAsync(categoryId), Times.Once);
        }
    }
}
