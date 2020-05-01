using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineShop_AspNetCore_ReactJS.Controllers;
using OnlineShop_AspNetCore_ReactJS.Helpers;
using OnlineShop_AspNetCore_ReactJS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Entities = OnlineShop_AspNetCore_ReactJS.Data.Entities;

namespace OnlineShop_AspNetCore_ReactJS.Tests.Controllers
{
    public class ShoppingCartControllerTests
    {
        private readonly string shoppingCartId = Guid.NewGuid().ToString();
        private readonly Mock<IShoppingCartService> mockShoppingCartService;
        private readonly Mock<IPieService> mockPieService;
        private readonly Mock<IMapper> mockMapper;
        private readonly ShoppingCartController sut;
        private readonly IEnumerable<Entities.Pie> pies;
        private readonly IEnumerable<Entities.ShoppingCartItem> shoppingCartItems;

        public ShoppingCartControllerTests()
        {
            // Arrange
            mockShoppingCartService = new Mock<IShoppingCartService>();
            mockPieService = new Mock<IPieService>();
            mockMapper = new Mock<IMapper>();
            sut = new ShoppingCartController(mockShoppingCartService.Object, mockPieService.Object, mockMapper.Object);

            var category1 = new Entities.Category { Id = 1, Name = "Category1", Description = "Category1_Description" };
            var category2 = new Entities.Category { Id = 2, Name = "Category2", Description = "Category2_Description" };
            var category3 = new Entities.Category { Id = 3, Name = "Category3", Description = "Category3_Description" };

            var pie1 = new Entities.Pie { Id = 1, Name = "Pie1", ShortDescription = "Pie1_ShortDescription", LongDescription = "Pie1_LongDescription", Price = 10.99m, IsPieOfTheWeek = true, InStock = true, ImageUrl = "Pie1_ImageUrl", ThumbnailImageUrl = "Pie1_ThumbnailImageUrl", Category = category1 };
            var pie2 = new Entities.Pie { Id = 2, Name = "Pie2", ShortDescription = "Pie2_ShortDescription", LongDescription = "Pie2_LongDescription", Price = 11.50m, IsPieOfTheWeek = false, InStock = false, ImageUrl = "Pie2_ImageUrl", ThumbnailImageUrl = "Pie2_ThumbnailImageUrl", Category = category2 };
            var pie3 = new Entities.Pie { Id = 3, Name = "Pie3", ShortDescription = "Pie3_ShortDescription", LongDescription = "Pie3_LongDescription", Price = 12.59m, IsPieOfTheWeek = false, InStock = true, ImageUrl = "Pie3_ImageUrl", ThumbnailImageUrl = "Pie3_ThumbnailImageUrl", Category = category3 };
            var pie4 = new Entities.Pie { Id = 4, Name = "Pie4", ShortDescription = "Pie4_ShortDescription", LongDescription = "Pie4_LongDescription", Price = 15.95m, IsPieOfTheWeek = false, InStock = true, ImageUrl = "Pie4_ImageUrl", ThumbnailImageUrl = "Pie4_ThumbnailImageUrl", Category = category1 };
            var pie5 = new Entities.Pie { Id = 5, Name = "Pie5", ShortDescription = "Pie5_ShortDescription", LongDescription = "Pie5_LongDescription", Price = 16.95m, IsPieOfTheWeek = true, InStock = true, ImageUrl = "Pie5_ImageUrl", ThumbnailImageUrl = "Pie5_ThumbnailImageUrl", Category = category2 };
            var pie6 = new Entities.Pie { Id = 6, Name = "Pie6", ShortDescription = "Pie6_ShortDescription", LongDescription = "Pie6_LongDescription", Price = 21.40m, IsPieOfTheWeek = false, InStock = false, ImageUrl = "Pie6_ImageUrl", ThumbnailImageUrl = "Pie6_ThumbnailImageUrl", Category = category3 };

            pies = new Entities.Pie[] { pie1, pie2, pie3, pie4, pie5, pie6 };

            shoppingCartItems = new Entities.ShoppingCartItem[]
            {
                new Entities.ShoppingCartItem { Id = 1, Pie = pie1, Quantity = 1, ShoppingCartId = shoppingCartId },
                new Entities.ShoppingCartItem { Id = 2, Pie = pie2, Quantity = 2, ShoppingCartId = shoppingCartId },
                new Entities.ShoppingCartItem { Id = 3, Pie = pie3, Quantity = 1, ShoppingCartId = shoppingCartId },
                new Entities.ShoppingCartItem { Id = 4, Pie = pie4, Quantity = 4, ShoppingCartId = shoppingCartId },
                new Entities.ShoppingCartItem { Id = 5, Pie = pie5, Quantity = 1, ShoppingCartId = shoppingCartId },
                new Entities.ShoppingCartItem { Id = 6, Pie = pie6, Quantity = 6, ShoppingCartId = shoppingCartId }
            };
        }

        private IEnumerable<Entities.ShoppingCartItem> SeedData()
        {
            return shoppingCartItems;
        }

        private Entities.Pie SeedPieData(int pieId)
        {
            return pies.FirstOrDefault(p => p.Id == pieId);
        }

        private Models.ShoppingCartItem MapData(Entities.ShoppingCartItem shoppingCartItem)
        {
            return new Models.ShoppingCartItem
            {
                Id = shoppingCartItem.Id,
                Quantity = shoppingCartItem.Quantity,
                PieId = shoppingCartItem.Pie.Id,
                PieName = shoppingCartItem.Pie.Name,
                PiePrice = shoppingCartItem.Pie.Price,
                PieThumbnailImageUrl = shoppingCartItem.Pie.ThumbnailImageUrl,
            };
        }

        private IEnumerable<Models.ShoppingCartItem> MapData(IEnumerable<Entities.ShoppingCartItem> shoppingCartItems)
        {
            return shoppingCartItems.Select(i => MapData(i)).ToArray();
        }

        [Fact]
        public async Task GetShoppingCartItemsAsync_WhenCalled_ReturnsAllShoppingCartItems()
        {
            // Arrange
            var seedData = SeedData();
            var mappedData = MapData(seedData);

            mockShoppingCartService.Setup(m => m.GetShoppingCartItemsAsync()).ReturnsAsync(seedData);
            mockMapper.Setup(m => m.Map<Models.ShoppingCartItem[]>(seedData)).Returns(mappedData.ToArray());

            // Act
            var result = await sut.GetShoppingCartItemsAsync();

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<IEnumerable<Models.ShoppingCartItem>>>(result);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Models.ShoppingCartItem>>(okObjectResult.Value);
            Assert.Equal(6, model.Count());

            mockShoppingCartService.Verify(m => m.GetShoppingCartItemsAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateShoppingCartItemsAsync_WhenCalledWithInvalidPieId_ReturnsBadRequest()
        {
            // Arrange
            int pieId = 7;
            var pie = default(Entities.Pie);
            var shoppingCartAction = new Models.ShoppingCartAction { PieId = pieId, Action = "INCREASE_ITEM_QUANTITY", Quantity = 1 };
            
            mockPieService.Setup(m => m.GetPieAsync(shoppingCartAction.PieId)).ReturnsAsync(pie);

            // Act
            var result = await sut.UpdateShoppingCartItemsAsync(shoppingCartAction);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<bool>>(result);
            var badRequestObjectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal(ErrorMessage.InvalidData(Constant.BadRequest, typeof(Models.Pie), Constant.Id, shoppingCartAction.PieId.ToString()), badRequestObjectResult.Value);

            mockPieService.Verify(m => m.GetPieAsync(shoppingCartAction.PieId), Times.Once);
        }

        [Fact]
        public async Task UpdateShoppingCartItemsAsync_WhenCalledWithInvalidShoppingCartAction_ReturnsBadRequest()
        {
            // Arrange
            int pieId = 1;
            var pie = SeedPieData(pieId);
            var shoppingCartAction = new Models.ShoppingCartAction { PieId = pieId, Action = "Invalid_Action", Quantity = 1 };
            
            mockPieService.Setup(m => m.GetPieAsync(shoppingCartAction.PieId)).ReturnsAsync(pie);

            // Act
            var result = await sut.UpdateShoppingCartItemsAsync(shoppingCartAction);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<bool>>(result);
            var badRequestObjectResult = Assert.IsAssignableFrom<BadRequestObjectResult>(actionResult.Result);
            Assert.Equal(ErrorMessage.InvalidData(Constant.BadRequest, typeof(Models.ShoppingCartAction), Constant.Action, shoppingCartAction.Action), badRequestObjectResult.Value);

            mockPieService.Verify(m => m.GetPieAsync(shoppingCartAction.PieId), Times.Never);
        }

        [Fact]
        public async Task UpdateShoppingCartItemsAsync_WhenCalledWithIncreaseItemQuantityShoppingCartAction_ReturnsTrue()
        {
            // Arrange
            int pieId = 1;
            var pie = SeedPieData(pieId);
            var shoppingCartAction = new Models.ShoppingCartAction { PieId = pieId, Action = "INCREASE_ITEM_QUANTITY", Quantity = 1 };

            mockPieService.Setup(m => m.GetPieAsync(shoppingCartAction.PieId)).ReturnsAsync(pie);
            mockShoppingCartService.Setup(m => m.IncreaseShoppingCartItemQuantityAsync(shoppingCartAction.PieId, shoppingCartAction.Quantity)).ReturnsAsync(1);

            // Act
            var result = await sut.UpdateShoppingCartItemsAsync(shoppingCartAction);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<bool>>(result);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<bool>(okObjectResult.Value);
            Assert.True(model);
            
            mockPieService.Verify(m => m.GetPieAsync(shoppingCartAction.PieId), Times.Once);
            mockShoppingCartService.Verify(m => m.IncreaseShoppingCartItemQuantityAsync(shoppingCartAction.PieId, shoppingCartAction.Quantity), Times.Once);
        }

        [Fact]
        public async Task UpdateShoppingCartItemsAsync_WhenCalledWithDecreaseItemQuantityShoppingCartActionAndPieIdInShoppingCart_ReturnsTrue()
        {
            // Arrange
            int pieId = 1;
            var pie = SeedPieData(pieId);
            var shoppingCartAction = new Models.ShoppingCartAction { PieId = pieId, Action = "DECREASE_ITEM_QUANTITY", Quantity = 1 };

            mockPieService.Setup(m => m.GetPieAsync(shoppingCartAction.PieId)).ReturnsAsync(pie);
            mockShoppingCartService.Setup(m => m.DecreaseShoppingCartItemQuantityAsync(shoppingCartAction.PieId, shoppingCartAction.Quantity)).ReturnsAsync(1);

            // Act
            var result = await sut.UpdateShoppingCartItemsAsync(shoppingCartAction);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<bool>>(result);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<bool>(okObjectResult.Value);
            Assert.True(model);

            mockPieService.Verify(m => m.GetPieAsync(shoppingCartAction.PieId), Times.Once);
            mockShoppingCartService.Verify(m => m.DecreaseShoppingCartItemQuantityAsync(shoppingCartAction.PieId, shoppingCartAction.Quantity), Times.Once);
        }

        [Fact]
        public async Task UpdateShoppingCartItemsAsync_WhenCalledWithDecreaseItemQuantityShoppingCartActionAndPieIdNotInShoppingCart_ReturnsFalse()
        {
            // Arrange
            int pieId = 1;
            var pie = SeedPieData(pieId);
            var shoppingCartAction = new Models.ShoppingCartAction { PieId = pieId, Action = "DECREASE_ITEM_QUANTITY", Quantity = 1 };

            mockPieService.Setup(m => m.GetPieAsync(shoppingCartAction.PieId)).ReturnsAsync(pie);
            mockShoppingCartService.Setup(m => m.DecreaseShoppingCartItemQuantityAsync(shoppingCartAction.PieId, shoppingCartAction.Quantity)).ReturnsAsync(0);

            // Act
            var result = await sut.UpdateShoppingCartItemsAsync(shoppingCartAction);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<bool>>(result);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<bool>(okObjectResult.Value);
            Assert.False(model);

            mockPieService.Verify(m => m.GetPieAsync(shoppingCartAction.PieId), Times.Once);
            mockShoppingCartService.Verify(m => m.DecreaseShoppingCartItemQuantityAsync(shoppingCartAction.PieId, shoppingCartAction.Quantity), Times.Once);
        }

        [Fact]
        public async Task UpdateShoppingCartItemsAsync_WhenCalledWithRemoveItemShoppingCartActionAndPieIdInShoppingCart_ReturnsTrue()
        {
            // Arrange
            int pieId = 1;
            var pie = SeedPieData(pieId);
            var shoppingCartAction = new Models.ShoppingCartAction { PieId = pieId, Action = "REMOVE_ITEM", Quantity = 1 };

            mockPieService.Setup(m => m.GetPieAsync(shoppingCartAction.PieId)).ReturnsAsync(pie);
            mockShoppingCartService.Setup(m => m.RemoveShoppingCartItemAsync(shoppingCartAction.PieId)).ReturnsAsync(1);

            // Act
            var result = await sut.UpdateShoppingCartItemsAsync(shoppingCartAction);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<bool>>(result);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<bool>(okObjectResult.Value);
            Assert.True(model);

            mockPieService.Verify(m => m.GetPieAsync(shoppingCartAction.PieId), Times.Once);
            mockShoppingCartService.Verify(m => m.RemoveShoppingCartItemAsync(shoppingCartAction.PieId), Times.Once);
        }

        [Fact]
        public async Task UpdateShoppingCartItemsAsync_WhenCalledWithRemoveItemShoppingCartActionAndPieIdNotInShoppingCart_ReturnsFalse()
        {
            // Arrange
            int pieId = 1;
            var pie = SeedPieData(pieId);
            var shoppingCartAction = new Models.ShoppingCartAction { PieId = pieId, Action = "REMOVE_ITEM", Quantity = 1 };

            mockPieService.Setup(m => m.GetPieAsync(shoppingCartAction.PieId)).ReturnsAsync(pie);
            mockShoppingCartService.Setup(m => m.RemoveShoppingCartItemAsync(shoppingCartAction.PieId)).ReturnsAsync(0);

            // Act
            var result = await sut.UpdateShoppingCartItemsAsync(shoppingCartAction);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<bool>>(result);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<bool>(okObjectResult.Value);
            Assert.False(model);

            mockPieService.Verify(m => m.GetPieAsync(shoppingCartAction.PieId), Times.Once);
            mockShoppingCartService.Verify(m => m.RemoveShoppingCartItemAsync(shoppingCartAction.PieId), Times.Once);
        }

        [Fact]
        public async Task UpdateShoppingCartItemsAsync_WhenCalledWithClearCartShoppingCartActionWithNonEmptyShoppingCart_ReturnsTrue()
        {
            // Arrange
            int pieId = 1;
            var pie = SeedPieData(pieId);
            var shoppingCartAction = new Models.ShoppingCartAction { PieId = pieId, Action = "CLEAR_CART", Quantity = 1 };

            mockPieService.Setup(m => m.GetPieAsync(shoppingCartAction.PieId)).ReturnsAsync(pie);
            mockShoppingCartService.Setup(m => m.ClearShoppingCartAsync()).ReturnsAsync(1);

            // Act
            var result = await sut.UpdateShoppingCartItemsAsync(shoppingCartAction);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<bool>>(result);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<bool>(okObjectResult.Value);
            Assert.True(model);

            mockPieService.Verify(m => m.GetPieAsync(shoppingCartAction.PieId), Times.Never);
            mockShoppingCartService.Verify(m => m.ClearShoppingCartAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateShoppingCartItemsAsync_WhenCalledWithClearCartShoppingCartActionWithEmptyShoppingCart_ReturnsFalse()
        {
            // Arrange
            int pieId = 1;
            var pie = SeedPieData(pieId);
            var shoppingCartAction = new Models.ShoppingCartAction { PieId = pieId, Action = "CLEAR_CART", Quantity = 1 };

            mockPieService.Setup(m => m.GetPieAsync(shoppingCartAction.PieId)).ReturnsAsync(pie);
            mockShoppingCartService.Setup(m => m.ClearShoppingCartAsync()).ReturnsAsync(0);

            // Act
            var result = await sut.UpdateShoppingCartItemsAsync(shoppingCartAction);

            // Assert
            var actionResult = Assert.IsAssignableFrom<ActionResult<bool>>(result);
            var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<bool>(okObjectResult.Value);
            Assert.False(model);

            mockPieService.Verify(m => m.GetPieAsync(shoppingCartAction.PieId), Times.Never);
            mockShoppingCartService.Verify(m => m.ClearShoppingCartAsync(), Times.Once);
        }
    }
}
