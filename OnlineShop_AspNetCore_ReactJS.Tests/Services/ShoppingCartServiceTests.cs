using Microsoft.EntityFrameworkCore;
using OnlineShop_AspNetCore_ReactJS.Data;
using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using OnlineShop_AspNetCore_ReactJS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShop_AspNetCore_ReactJS.Tests.Services
{
    public class ShoppingCartServiceTests : ServiceTestsBase
    {
        private readonly string shoppingCartId = Guid.NewGuid().ToString();
        
        public ShoppingCartServiceTests()
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
            var pie7 = new Pie { Id = 7, Name = "Pie7", ShortDescription = "Pie7_ShortDescription", LongDescription = "Pie7_LongDescription", Price = 13.95m, IsPieOfTheWeek = false, InStock = true, ImageUrl = "Pie7_ImageUrl", ThumbnailImageUrl = "Pie7_ThumbnailImageUrl", Category = category2 };
            var pie8 = new Pie { Id = 8, Name = "Pie8", ShortDescription = "Pie8_ShortDescription", LongDescription = "Pie8_LongDescription", Price = 14.85m, IsPieOfTheWeek = true, InStock = false, ImageUrl = "Pie8_ImageUrl", ThumbnailImageUrl = "Pie8_ThumbnailImageUrl", Category = category3 };
            var pie9 = new Pie { Id = 9, Name = "Pie9", ShortDescription = "Pie9_ShortDescription", LongDescription = "Pie9_LongDescription", Price = 14.85m, IsPieOfTheWeek = true, InStock = false, ImageUrl = "Pie9_ImageUrl", ThumbnailImageUrl = "Pie9_ThumbnailImageUrl", Category = category1 };

            var shoppingCartItem1 = new ShoppingCartItem { Id = 1, Pie = pie1, Quantity = 1, ShoppingCartId = shoppingCartId };
            var shoppingCartItem2 = new ShoppingCartItem { Id = 2, Pie = pie2, Quantity = 2, ShoppingCartId = shoppingCartId };
            var shoppingCartItem3 = new ShoppingCartItem { Id = 3, Pie = pie3, Quantity = 1, ShoppingCartId = shoppingCartId };
            var shoppingCartItem4 = new ShoppingCartItem { Id = 4, Pie = pie4, Quantity = 4, ShoppingCartId = shoppingCartId };
            var shoppingCartItem5 = new ShoppingCartItem { Id = 5, Pie = pie5, Quantity = 1, ShoppingCartId = shoppingCartId };
            var shoppingCartItem6 = new ShoppingCartItem { Id = 6, Pie = pie6, Quantity = 6, ShoppingCartId = shoppingCartId };

            using (var context = new OnlineShopContext(options))
            {
                context.Category.AddRange(category1, category2, category3);
                context.Pie.AddRange(pie1, pie2, pie3, pie4, pie5, pie6, pie7, pie8, pie9);
                context.ShoppingCartItem.AddRange(shoppingCartItem1, shoppingCartItem2, shoppingCartItem3, shoppingCartItem4, shoppingCartItem5, shoppingCartItem6);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetShoppingCartItemsAsync_WhenCalled_ReturnsAllShoppingCartItemsWithPie()
        {
            // Act
            List<ShoppingCartItem> shoppingCartItems = null;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new ShoppingCartService(context, shoppingCartId);
                shoppingCartItems = (await sut.GetShoppingCartItemsAsync()).ToList();
            }

            // Assert
            Assert.NotNull(shoppingCartItems);
            Assert.Equal(6, shoppingCartItems.Count());
            Assert.NotNull(shoppingCartItems[0].Pie);
            Assert.NotNull(shoppingCartItems[1].Pie);
            Assert.NotNull(shoppingCartItems[2].Pie);
            Assert.NotNull(shoppingCartItems[3].Pie);
            Assert.NotNull(shoppingCartItems[4].Pie);
            Assert.NotNull(shoppingCartItems[5].Pie);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public async Task GetShoppingCartItemAsync_WhenCalledWithPieId_ReturnsShoppingCartItemWithPieIdWithPie(int pieId)
        {
            // Act
            ShoppingCartItem shoppingCartItem = null;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new ShoppingCartService(context, shoppingCartId);
                shoppingCartItem = await sut.GetShoppingCartItemAsync(pieId);
            }

            // Assert
            Assert.NotNull(shoppingCartItem);
            Assert.Equal(pieId, shoppingCartItem.PieId);
            Assert.NotNull(shoppingCartItem.Pie);
        }

        [Theory]
        [InlineData(1, 1, 1, 2)]
        [InlineData(2, 1, 1, 3)]
        [InlineData(3, 1, 1, 2)]
        [InlineData(4, 1, 1, 5)]
        [InlineData(5, 1, 1, 2)]
        [InlineData(6, 1, 1, 7)]
        [InlineData(7, 1, 1, 1)]
        [InlineData(8, 1, 1, 1)]
        [InlineData(9, 1, 1, 1)]
        public async Task IncreaseShoppingCartItemQuantityAsync_WhenCalled_Returns1AndIncreasesQuantity(int pieId, int quantity, int expectedOutput, int expectedQuantity)
        {
            // Act
            int numberOfRecordsUpdated = 0;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new ShoppingCartService(context, shoppingCartId);
                numberOfRecordsUpdated = await sut.IncreaseShoppingCartItemQuantityAsync(pieId, quantity);
            }

            // Assert
            ShoppingCartItem shoppingCartItem = null;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new ShoppingCartService(context, shoppingCartId);
                shoppingCartItem = await sut.GetShoppingCartItemAsync(pieId);
            }

            Assert.Equal(expectedOutput, numberOfRecordsUpdated);
            Assert.NotNull(shoppingCartItem);
            Assert.Equal(expectedQuantity, shoppingCartItem.Quantity);
        }

        [Theory]
        [InlineData(2, 1, 1, 1)]
        [InlineData(4, 1, 1, 3)]
        [InlineData(6, 1, 1, 5)]
        public async Task DecreaseShoppingCartItemQuantityAsync_WhenCalledWithPieIdInShoppingCartAndQuantityLessThanCurrentQuantity_Returns1AndDecreasesQuantity(int pieId, int quantity, int expectedOutput, int expectedQuantity)
        {
            // Act
            int numberOfRecordsUpdated = 0;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new ShoppingCartService(context, shoppingCartId);
                numberOfRecordsUpdated = await sut.DecreaseShoppingCartItemQuantityAsync(pieId, quantity);
            }

            // Assert
            ShoppingCartItem shoppingCartItem = null;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new ShoppingCartService(context, shoppingCartId);
                shoppingCartItem = await sut.GetShoppingCartItemAsync(pieId);
            }

            Assert.Equal(expectedOutput, numberOfRecordsUpdated);
            Assert.NotNull(shoppingCartItem);
            Assert.Equal(expectedQuantity, shoppingCartItem.Quantity);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(2, 2, 1)]
        [InlineData(3, 1, 1)]
        [InlineData(4, 4, 1)]
        [InlineData(5, 1, 1)]
        [InlineData(6, 6, 1)]
        public async Task DecreaseShoppingCartItemQuantityAsync_WhenCalledWithPieIdInShoppingCartAndQuantityEqualToCurrentQuantity_Returns1AndRemovesShoppingCartItem(int pieId, int quantity, int expectedOutput)
        {
            // Act
            int numberOfRecordsUpdated = 0;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new ShoppingCartService(context, shoppingCartId);
                numberOfRecordsUpdated = await sut.DecreaseShoppingCartItemQuantityAsync(pieId, quantity);
            }

            // Assert
            ShoppingCartItem shoppingCartItem = null;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new ShoppingCartService(context, shoppingCartId);
                shoppingCartItem = await sut.GetShoppingCartItemAsync(pieId);
            }

            Assert.Equal(expectedOutput, numberOfRecordsUpdated);
            Assert.Null(shoppingCartItem);
        }

        [Theory]
        [InlineData(7, 1, 0)]
        [InlineData(8, 1, 0)]
        [InlineData(9, 1, 0)]
        public async Task DecreaseShoppingCartItemQuantityAsync_WhenCalledWithPieIdNotInShoppingCart_Returns0(int pieId, int quantity, int expectedOutput)
        {
            // Act
            int numberOfRecordsUpdated = 0;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new ShoppingCartService(context, shoppingCartId);
                numberOfRecordsUpdated = await sut.DecreaseShoppingCartItemQuantityAsync(pieId, quantity);
            }

            // Assert
            Assert.Equal(expectedOutput, numberOfRecordsUpdated);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 1)]
        [InlineData(4, 1)]
        [InlineData(5, 1)]
        [InlineData(6, 1)]
        public async Task RemoveShoppingCartItemAsync_WhenCalledWithPieIdInShoppingCart_Returns1AndRemovesShoppingCartItem(int pieId, int expectedOutput)
        {
            // Act
            int numberOfRecordsUpdated = 0;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new ShoppingCartService(context, shoppingCartId);
                numberOfRecordsUpdated = await sut.RemoveShoppingCartItemAsync(pieId);
            }

            // Assert
            ShoppingCartItem shoppingCartItem = null;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new ShoppingCartService(context, shoppingCartId);
                shoppingCartItem = await sut.GetShoppingCartItemAsync(pieId);
            }

            Assert.Equal(expectedOutput, numberOfRecordsUpdated);
            Assert.Null(shoppingCartItem);
        }

        [Theory]
        [InlineData(7, 0)]
        [InlineData(8, 0)]
        [InlineData(9, 0)]
        public async Task RemoveShoppingCartItemAsync_WhenCalledWithPieIdNotInShoppingCart_Returns0(int pieId, int expectedOutput)
        {
            // Act
            int numberOfRecordsUpdated = 0;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new ShoppingCartService(context, shoppingCartId);
                numberOfRecordsUpdated = await sut.RemoveShoppingCartItemAsync(pieId);
            }

            // Assert
            Assert.Equal(expectedOutput, numberOfRecordsUpdated);
        }

        [Fact]
        public async Task ClearShoppingCartAsync_WhenCalledWithNonEmptyShoppingCart_Returns1AndEmptiesShoppingCart()
        {
            // Act
            int numberOfRecordsUpdated = 0;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new ShoppingCartService(context, shoppingCartId);
                numberOfRecordsUpdated = await sut.ClearShoppingCartAsync();
            }

            // Assert
            IEnumerable<ShoppingCartItem> shoppingCartItems = null;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new ShoppingCartService(context, shoppingCartId);
                shoppingCartItems = await sut.GetShoppingCartItemsAsync();
            }

            Assert.Equal(6, numberOfRecordsUpdated);
            Assert.Empty(shoppingCartItems);
        }

        [Fact]
        public async Task ClearShoppingCartAsync_WhenCalledWithEmptyShoppingCart_Returns0()
        {
            // Arrange - Empty Shopping Cart
            using (var context = new OnlineShopContext(options))
            {
                var shoppingCartItems = await context.ShoppingCartItem.ToListAsync();
                context.ShoppingCartItem.RemoveRange(shoppingCartItems);
                await context.SaveChangesAsync();
            }

            // Act
            int numberOfRecordsUpdated = 0;
            using (var context = new OnlineShopContext(options))
            {
                var sut = new ShoppingCartService(context, shoppingCartId);
                numberOfRecordsUpdated = await sut.ClearShoppingCartAsync();
            }

            // Assert
            Assert.Equal(0, numberOfRecordsUpdated);
        }
    }
}
