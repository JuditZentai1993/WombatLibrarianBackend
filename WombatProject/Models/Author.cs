using System.ComponentModel.DataAnnotations;

namespace WombatLibrarianApi.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
    }
}