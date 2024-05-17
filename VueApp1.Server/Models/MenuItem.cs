namespace VueApp1.Server.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        //public int ItemId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }

        public Menu Menu { get; set; }
        //public Item Item { get; set; }

    }
}
