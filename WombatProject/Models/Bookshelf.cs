using System.ComponentModel.DataAnnotations;

namespace WombatLibrarianApi.Models
{
    public class Bookshelf
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string BookId { get; set; }

        public Book book;
    }
}
