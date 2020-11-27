using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WombatLibrarianApi.Models
{
    public class AuthorContext : DbContext
    {
        public DbSet<Book> AuthorBookItems { get; set; }
        private readonly IConfiguration Configuration;

        public AuthorContext(DbContextOptions<AuthorContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        public async Task<string> GetAuthorBooks(string author)
        {
            clearBookItems();
            string url = $"{Configuration["GBooksURL"]}?q=inauthor:{author}";

            using (var client = new HttpClient())
            {
                var uri = new Uri(url);

                var response = await client.GetAsync(uri);

                string textResult = await response.Content.ReadAsStringAsync();

                JObject bookSearch = JObject.Parse(textResult);

                IList<JToken> tokens = bookSearch["items"].Children().ToList();
                foreach (JToken token in tokens)
                {
                    AuthorBookItems.Add(JsonHelper.parseJsonToken(token));
                }
                await SaveChangesAsync();

                return textResult;
            }
        }

        private void clearBookItems()
        {
            AuthorBookItems.RemoveRange(AuthorBookItems);
            SaveChanges();
        }
    }
}
