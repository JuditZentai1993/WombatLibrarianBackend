using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WombatLibrarianApi.Models
{
    public class Book
    {
        [Required]
        public string Id { get; set; }
        [StringLength(1000)]
        public string Title { get; set; }
        [StringLength(10000)]
        public string Subtitle { get; set; }
        public string Thumbnail { get; set; }
        [StringLength(100000)]
        public string Description { get; set; }
        public int PageCount { get; set; }
        public double Rating { get; set; } = 0;
        public double RatingCount { get; set; } = 0;
        public string Language { get; set; }
        public string MaturityRating { get; set; }
        public string Published { get; set; }
        public string Publisher { get; set; }
    
        public List<Author> Authors { get; set; }
        public List<Category> Categories { get; set; }
    }
}
