using Microsoft.EntityFrameworkCore;

namespace VueApp1.Server.Models
{
    public class RepoContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Person> Persons { get; set; }

        public RepoContext(DbContextOptions<RepoContext> options) :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
