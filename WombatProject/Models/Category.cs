using System.ComponentModel.DataAnnotations;

namespace WombatLibrarianApi.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [StringLength(1000)]
        public string Name { get; set; }
    }
}