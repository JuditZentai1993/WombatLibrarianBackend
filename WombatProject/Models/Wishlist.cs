using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WombatLibrarianApi.Models
{
    public class Wishlist
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string BookId { get; set; }

        public Book book;
    }
}
