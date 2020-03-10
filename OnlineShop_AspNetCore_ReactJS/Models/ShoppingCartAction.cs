using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop_AspNetCore_ReactJS.Models
{
    public class ShoppingCartAction
    {
        public int PieId { get; set; }

        public int Quantity { get; set; } = 1;

        public string Action { get; set; }
    }
}
