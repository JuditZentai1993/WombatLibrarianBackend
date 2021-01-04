using System.Collections.Generic;
using System.Text.Json;

namespace WombatLibrarianApi.Models
{
    public class Book
    {
        public string Id { get; set; }
        public List<Author> Authors { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public double Rating { get; set; }
        public double RatingCount { get; set; }
        public string Language { get; set; }
        public List<Category> Categories { get; set; }
        public string MaturityRating { get; set; }
        public string Published { get; set; }
        public string Publisher { get; set; }
    
        public override string ToString() => JsonSerializer.Serialize<Book>(this);
    }
}
