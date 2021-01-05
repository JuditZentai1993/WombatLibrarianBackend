using Microsoft.EntityFrameworkCore;

namespace WombatLibrarianApi.Models
{
    public class WombatBooksContext : DbContext
    {
        public WombatBooksContext(DbContextOptions<WombatBooksContext> options) : base(options)
        {
        }

        public DbSet<Book> BookShelfItems { get; set; }
        public DbSet<Book> WishListItems { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
