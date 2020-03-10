using Microsoft.EntityFrameworkCore;
using OnlineShop_AspNetCore_ReactJS.Data;
using OnlineShop_AspNetCore_ReactJS.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly OnlineShopContext context;
        private readonly string shoppingCartId;

        public ShoppingCartService(OnlineShopContext context, string shoppingCartId)
        {
            this.context = context;
            this.shoppingCartId = shoppingCartId;
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsAsync()
        {
            return await context.ShoppingCartItem.Where(i => i.ShoppingCartId == shoppingCartId).Include(i => i.Pie).ToListAsync();
        }
        
        public async Task<int> IncreaseShoppingCartItemQuantityAsync(int pieId, int quantity)
        {
            var shoppingCartItem = await context.ShoppingCartItem.FirstOrDefaultAsync(i => i.ShoppingCartId == shoppingCartId && i.PieId == pieId);
            if(shoppingCartItem == null)
            {
                context.ShoppingCartItem.Add(new ShoppingCartItem { ShoppingCartId = shoppingCartId, PieId = pieId, Quantity = quantity });
            }
            else
            {
                shoppingCartItem.Quantity += quantity;
                context.Entry(shoppingCartItem).State = EntityState.Modified;
            }
            
            int count = await context.SaveChangesAsync();
            return count;
        }

        public async Task<int> DecreaseShoppingCartItemQuantityAsync(int pieId, int quantity)
        {
            var shoppingCartItem = await context.ShoppingCartItem.FirstOrDefaultAsync(i => i.ShoppingCartId == shoppingCartId && i.PieId == pieId);
            if (shoppingCartItem != null)
            {
                shoppingCartItem.Quantity -= quantity;
                if(shoppingCartItem.Quantity <= 0)
                {
                    context.Entry(shoppingCartItem).State = EntityState.Deleted;
                }
                else
                {
                    context.Entry(shoppingCartItem).State = EntityState.Modified;
                }
            }

            int count = await context.SaveChangesAsync();
            return count;
        }

        public async Task<int> RemoveShoppingCartItemAsync(int pieId)
        {
            var shoppingCartItem = await context.ShoppingCartItem.FirstOrDefaultAsync(i => i.ShoppingCartId == shoppingCartId && i.PieId == pieId);
            if (shoppingCartItem != null)
            {
                context.Entry(shoppingCartItem).State = EntityState.Deleted;
            }

            int count = await context.SaveChangesAsync();
            return count;
        }

        public async Task<int> ClearShoppingCartAsync()
        {
            var shoppingCartItems = await context.ShoppingCartItem.Where(i => i.ShoppingCartId == shoppingCartId).ToListAsync();
            if (shoppingCartItems != null && shoppingCartItems.Count > 0)
            {
                context.ShoppingCartItem.RemoveRange(shoppingCartItems);
            }

            int count = await context.SaveChangesAsync();
            return count;
        }
    }
}
