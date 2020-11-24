using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WombatLibrarianApi.Models
{
    public class BookContext : DbContext
    {
        public DbSet<Book> BookItems { get; set; }

        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }

        public async Task<string> GetSearchResults(string searchTerm)
        {
            string url = "https://www.googleapis.com/books/v1/volumes?q=" + searchTerm + "&maxResults=40";

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
