using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WombatLibrarianApi.Models
{
    public class BookContext : DbContext
    {
        public DbSet<Book> TodoItems { get; set; }

        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }
    }
}
