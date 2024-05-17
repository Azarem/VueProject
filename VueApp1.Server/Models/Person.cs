namespace VueApp1.Server.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Menu> Menus { get; set; }
    }
}
