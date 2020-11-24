using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WombatLibrarianApi.Models
{
    public class AuthorContext : DbContext
    {
        public DbSet<Book> AuthorBookItems { get; set; }

        public AuthorContext(DbContextOptions<AuthorContext> options) : base(options)
        {
        }

        public async Task<string> GetAuthorBooks(string author, int startIndex, 
            int maxResultsPerRequest)
        {
            string url = "https://www.googleapis.com/books/v1/volumes?q=inauthor:" + author
                            + "&startIndex=" + startIndex.ToString() + "&maxResults=" + maxResultsPerRequest.ToString();

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
