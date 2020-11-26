using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace WombatLibrarianApi.Models
{
    public class BookContext : DbContext
    {
        public DbSet<Book> BookItems { get; set; }
        private readonly IConfiguration Configuration;

        public BookContext(DbContextOptions<BookContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        public async Task GetSearchResults(string searchTerm)
        {
            clearBookItems();
            string url = $"{Configuration["GBooksURL"]}?q={searchTerm}&maxResults=10";

            using (var client = new HttpClient())
            {
                var uri = new Uri(url);

                var response = await client.GetAsync(uri);

                string textResult = await response.Content.ReadAsStringAsync();

                JObject bookSearch = JObject.Parse(textResult);

                IList<JToken> tokens = bookSearch["items"].Children().ToList();
                foreach (JToken token in tokens)
                {
                    BookItems.Add(parseJsonToken(token));
                }
                await SaveChangesAsync();
            }
        }

        private void clearBookItems()
        {
            BookItems.RemoveRange(BookItems);
            SaveChanges();
        }

        private Book parseJsonToken(JToken jToken)
        {

            JObject volumeInfo = (JObject)jToken["volumeInfo"];
        
            Book book = new Book();
            book.Id = jToken["id"]?.ToString();
            book.Title = volumeInfo["title"]?.ToString();
            book.Subtitle = volumeInfo["subtitle"]?.ToString();
            book.Thumbnail = volumeInfo["imageLinks"]["thumbnail"]?.ToString();
            if (volumeInfo["authors"] != null)
            {
                book.Authors = JsonConvert.DeserializeObject<IEnumerable<string>>((volumeInfo["authors"]).ToString())
                    .Select(a => new Author{Name = a}).ToList();
            }
            book.Description = volumeInfo["description"]?.ToString();
            book.PageCount = volumeInfo["pageCount"] == null ? 0 : int.Parse( volumeInfo["pageCount"].ToString());
            book.Rating = volumeInfo["averageRating"] == null ? 0 : double.Parse( volumeInfo["averageRating"].ToString());
            book.RatingCount = volumeInfo["ratingsCount"] == null ? 0 : double.Parse( volumeInfo["ratingsCount"].ToString());
            book.Language = volumeInfo["language"]?.ToString();
            if (volumeInfo["categories"] != null)
            {
                book.Categories = JsonConvert.DeserializeObject<IEnumerable<string>>((volumeInfo["categories"]).ToString())
                    .Select(c => new Category{Name = c}).ToList();
            }
            book.MaturityRating = volumeInfo["maturityRating"]?.ToString();
            book.Published = volumeInfo["publishedDate"]?.ToString();
            book.Publisher = volumeInfo["publisher"]?.ToString();
            return book;
        }
    }
}
