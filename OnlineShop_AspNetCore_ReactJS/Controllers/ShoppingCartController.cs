using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop_AspNetCore_ReactJS.Services;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IPieService pieService;
        private readonly IMapper mapper;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IPieService pieService, IMapper mapper)
        {
            this.shoppingCartService = shoppingCartService;
            this.pieService = pieService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Models.ShoppingCartItem[]>> GetShoppingCartItemsAsync()
        {
            var items = await shoppingCartService.GetShoppingCartItemsAsync();
            return mapper.Map<Models.ShoppingCartItem[]>(items);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> UpdateShoppingCartItemsAsync(Models.ShoppingCartAction shoppingCartAction)
        {
            int count = 0;
            if (shoppingCartAction.Action.ToUpperInvariant() == "CLEAR_CART")
            {
                count = await shoppingCartService.ClearShoppingCartAsync();
            }
            else if (shoppingCartAction.Action.ToUpperInvariant() == "INCREASE_ITEM_QUANTITY"
                || shoppingCartAction.Action.ToUpperInvariant() == "DECREASE_ITEM_QUANTITY"
                || shoppingCartAction.Action.ToUpperInvariant() == "REMOVE_ITEM")
            {
                var pie = await pieService.GetPieAsync(shoppingCartAction.PieId);
                if (pie == null)
                {
                    return BadRequest($"PieId {shoppingCartAction.PieId} is invalid!");
                }

                if (shoppingCartAction.Action.ToUpperInvariant() == "INCREASE_ITEM_QUANTITY")
                {
                    count = await shoppingCartService.IncreaseShoppingCartItemQuantityAsync(shoppingCartAction.PieId, shoppingCartAction.Quantity);
                }
                else if (shoppingCartAction.Action.ToUpperInvariant() == "DECREASE_ITEM_QUANTITY")
                {
                    count = await shoppingCartService.DecreaseShoppingCartItemQuantityAsync(shoppingCartAction.PieId, shoppingCartAction.Quantity);
                }
                else if (shoppingCartAction.Action.ToUpperInvariant() == "REMOVE_ITEM")
                {
                    count = await shoppingCartService.RemoveShoppingCartItemAsync(shoppingCartAction.PieId);
                }
            }
            else
            {
                return BadRequest($"ShoppingCart Action {shoppingCartAction.Action} is invalid!");
            }

            return count > 0;
        }
    }
}
