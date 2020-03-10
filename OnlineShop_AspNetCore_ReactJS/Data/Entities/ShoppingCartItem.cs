namespace OnlineShop_AspNetCore_ReactJS.Data.Entities
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        
        public string ShoppingCartId { get; set; }

        public int Quantity { get; set; }

        public int PieId { get; set; }

        public Pie Pie { get; set; }
    }
}
