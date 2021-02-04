using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WombatLibrarianApi.Models
{
    public class WombatBooksContext : DbContext
    {
        private IConfiguration _configuration;
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Bookshelf> Bookshelves { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        public WombatBooksContext(DbContextOptions<WombatBooksContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("databaseConnection"));
    }
}
