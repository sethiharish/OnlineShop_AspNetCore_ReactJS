namespace OnlineShop_AspNetCore_ReactJS.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int PieId { get; set; }

        public string PieName { get; set; }

        public decimal PiePrice { get; set; }

        public string PieThumbnailImageUrl { get; set; }
    }
}
