using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
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
            string url = $"{Configuration["GBooksURL"]}?q=inauthor:{author}";

            using (var client = new HttpClient())
            {
                var uri = new Uri(url);

                var response = await client.GetAsync(uri);

                string textResult = await response.Content.ReadAsStringAsync();
                return textResult;
            }
        }
    }
}
