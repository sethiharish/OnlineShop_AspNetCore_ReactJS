using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop_AspNetCore_ReactJS.Helpers;
using OnlineShop_AspNetCore_ReactJS.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Controllers
{
    [Produces("application/json")]
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

        /// <summary>
        /// Get Shopping Cart Items for a specific user
        /// </summary>
        /// <returns>Returns Shopping Cart Items for a specific user</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.ShoppingCartItem>>> GetShoppingCartItemsAsync()
        {
            var items = await shoppingCartService.GetShoppingCartItemsAsync();
            return Ok(mapper.Map<Models.ShoppingCartItem[]>(items));
        }

        /// <summary>
        /// Update Shopping Cart Items for a specific user
        /// </summary>
        /// <remarks>
        /// Sample POST requests:
        ///     
        ///     {
        ///        "pieId": 1,
        ///        "quantity": 1,
        ///        "action": "INCREASE_ITEM_QUANTITY"
        ///     }
        ///
        ///     {
        ///        "pieId": 1,
        ///        "quantity": 1,
        ///        "action": "DECREASE_ITEM_QUANTITY"
        ///     }
        ///
        ///     {
        ///        "pieId": 1,
        ///        "action": "REMOVE_ITEM"
        ///     }
        ///
        ///     {
        ///        "action": "CLEAR_CART"
        ///     }
        ///
        ///     "pieId" and "quantity" are optional parameters
        ///     
        ///     "quantity" >= 1 (If not pased, defaults to 1)
        ///     
        ///     "action" = "INCREASE_ITEM_QUANTITY" / "DECREASE_ITEM_QUANTITY" / "REMOVE_ITEM" / "CLEAR_CART"
        ///     
        ///     Returns true for successful update and false for no update i.e. action performed against an item that didn't exist in the cart
        /// </remarks>
        /// <param name="shoppingCartAction"></param>
        /// <returns>Returns true for successful update and false for no update</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                    return BadRequest(ErrorMessage.InvalidData(Constant.BadRequest, typeof(Models.Pie), Constant.Id, shoppingCartAction.PieId.ToString()));
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
                return BadRequest(ErrorMessage.InvalidData(Constant.BadRequest, typeof(Models.ShoppingCartAction), Constant.Action, shoppingCartAction.Action));
            }

            return Ok(count > 0);
        }
    }
}
