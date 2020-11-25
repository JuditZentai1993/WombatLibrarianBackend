using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace WombatLibrarianApi.Models
{
    public class BookShelfContext : DbContext
    {
        public DbSet<Book> BookShelfItems { get; set; }
        public DbSet<Author> Authors { get; set; }

        public BookShelfContext(DbContextOptions<BookShelfContext> options) : base(options)
        {
        }
    }
}
