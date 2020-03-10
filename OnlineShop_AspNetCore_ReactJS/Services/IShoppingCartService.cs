using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Services
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsAsync();

        Task<int> IncreaseShoppingCartItemQuantityAsync(int pieId, int quantity);

        Task<int> DecreaseShoppingCartItemQuantityAsync(int pieId, int quantity);

        Task<int> RemoveShoppingCartItemAsync(int pieId);

        Task<int> ClearShoppingCartAsync();
    }
}
