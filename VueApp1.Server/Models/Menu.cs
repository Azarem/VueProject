namespace VueApp1.Server.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string? Title { get; set; }

        public decimal? TotalPrice { get; set; }
        public decimal? TotalPaid { get; set; }

        public Person Person { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
