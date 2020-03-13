namespace OnlineShop_AspNetCore_ReactJS.Data.Entities
{
    public class WorkItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int IterationId { get; set; }

        public Iteration Iteration { get; set; }
    }
}
