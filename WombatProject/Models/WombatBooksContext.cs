using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WombatLibrarianApi.Models
{
    public class WombatBooksContext : DbContext
    {
        public WombatBooksContext(DbContextOptions<WombatBooksContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration;

        public DbSet<Book> BookShelfItems { get; set; }
        public DbSet<Book> WishListItems { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("databaseConnection"));
    }
}
