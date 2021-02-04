using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace WombatLibrarianApi.Models
{
    public class Book
    {
        [Required]
        public string Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(1000)]
        public string Subtitle { get; set; }
        public string Thumbnail { get; set; }
        [StringLength(10000)]
        public string Description { get; set; }
        public int PageCount { get; set; }
        public double Rating { get; set; }
        public double RatingCount { get; set; }
        public string Language { get; set; }
        public string MaturityRating { get; set; }
        public string Published { get; set; }
        public string Publisher { get; set; }
    
        public List<Author> Authors { get; set; }
        public List<Category> Categories { get; set; }
        
        public override string ToString() => JsonSerializer.Serialize<Book>(this);
    }
}
