using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WombatLibrarianApi.Models
{
    public class Bookshelf
    {
        public int Id { get; set; }
        public string BookId { get; set; }

        public Book book;
    }
}
