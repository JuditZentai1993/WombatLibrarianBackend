using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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
