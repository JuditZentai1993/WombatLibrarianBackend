using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WombatLibrarianApi.Models;

namespace WombatLibrarianApi.Services
{
    public class GoogleBooksAPIService : BookAPIService
    {
        private readonly IConfiguration _configuration;
        public WombatBooksContext Context { get; }
        public List<Book> AuthorBookItems { get; set; } = new List<Book>();
        public List<Book> BookItems { get; set; } = new List<Book>();

        public GoogleBooksAPIService(IConfiguration configuration, WombatBooksContext context)
        {
            _configuration = configuration;
            Context = context;
        }

        public async Task GetSearchResults(string searchTerm)
        {
            BookItems.Clear();
            string url = $"{_configuration["GBooksURL"]}?q={searchTerm}&maxResults=40";
            IList<JToken> tokens = await GetBookItemsAsJToken(url);
            foreach (JToken token in tokens)
            {
                BookItems.Add(parseJsonToken(token));
            }
        }

        public async Task GetAuthorBooks(string author)
        {
            AuthorBookItems.Clear();
            string url = $"{_configuration["GBooksURL"]}?q=inauthor:{author}";
            IList<JToken> tokens = await GetBookItemsAsJToken(url);
            foreach (JToken token in tokens)
            {
                AuthorBookItems.Add(parseJsonToken(token));
            }
        }

        private async Task<IList<JToken>> GetBookItemsAsJToken(string url)
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(url);
                var response = await client.GetAsync(uri);
                string textResult = await response.Content.ReadAsStringAsync();
                JObject bookSearch = JObject.Parse(textResult);
                IList<JToken> tokens = bookSearch["items"].Children().ToList();
                return tokens;
            }
        }

        private Book parseJsonToken(JToken jToken)
        {

            JObject volumeInfo = (JObject)jToken["volumeInfo"];

            Book book = new Book();
            book.Id = jToken["id"]?.ToString();
            book.Title = volumeInfo["title"]?.ToString();
            book.Subtitle = volumeInfo["subtitle"]?.ToString();
            book.Thumbnail = volumeInfo["imageLinks"]?["thumbnail"]?.ToString();
            if (volumeInfo["authors"] != null)
            {
                book.Authors = JsonConvert.DeserializeObject<IEnumerable<string>>((volumeInfo["authors"]).ToString())
                    .Select(a => new Author { Name = a }).ToList();
            }
            book.Description = volumeInfo["description"]?.ToString();
            book.PageCount = volumeInfo["pageCount"] == null ? 0 : int.Parse(volumeInfo["pageCount"].ToString());
            book.Rating = volumeInfo["averageRating"] == null ? 0 : double.Parse(volumeInfo["averageRating"].ToString());
            book.RatingCount = volumeInfo["ratingsCount"] == null ? 0 : double.Parse(volumeInfo["ratingsCount"].ToString());
            book.Language = volumeInfo["language"]?.ToString();
            if (volumeInfo["categories"] != null)
            {
                book.Categories = JsonConvert.DeserializeObject<IEnumerable<string>>((volumeInfo["categories"]).ToString())
                    .Select(c => new Category { Name = c }).ToList();
            }
            book.MaturityRating = volumeInfo["maturityRating"]?.ToString();
            book.Published = volumeInfo["publishedDate"]?.ToString();
            book.Publisher = volumeInfo["publisher"]?.ToString();
            return book;
        }
    }
}
