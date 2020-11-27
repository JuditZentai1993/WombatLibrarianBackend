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
            string url = $"{Configuration["GBooksURL"]}?q={searchTerm}&maxResults=40";

            using (var client = new HttpClient())
            {
                var uri = new Uri(url);

                var response = await client.GetAsync(uri);

                string textResult = await response.Content.ReadAsStringAsync();

                JObject bookSearch = JObject.Parse(textResult);

                IList<JToken> tokens = bookSearch["items"].Children().ToList();
                foreach (JToken token in tokens)
                {
                    BookItems.Add(JsonHelper.parseJsonToken(token));
                }
                await SaveChangesAsync();
            }
        }

        private void clearBookItems()
        {
            BookItems.RemoveRange(BookItems);
            SaveChanges();
        }

        
    }
}
