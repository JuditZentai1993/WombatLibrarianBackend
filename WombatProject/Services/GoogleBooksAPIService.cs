using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WombatLibrarianApi.Models;
using WombatLibrarianApi.Settings;

namespace WombatLibrarianApi.Services
{
    public class GoogleBooksAPIService : IBookAPIService
    {
        protected readonly GoogleApiSettings _googleApiSettings;
        private readonly HttpClient _httpClient;

        public List<Book> AuthorBookItems { get; } = new List<Book>();
        public List<Book> SearchResults { get; } = new List<Book>();

        public GoogleBooksAPIService(
            IOptions<GoogleApiSettings> settings, 
            HttpClient httpClient)
        {
            this._googleApiSettings = settings.Value;
            this._httpClient = httpClient;
        }

        public async Task GetSearchResultsAsync(string searchTerm)
        {
            SearchResults.Clear();
            string url = $"{_googleApiSettings.GoogleBooksURL}?q={searchTerm}&maxResults=40";
            IList<JToken> tokens = await GetBookItemsAsJToken(url);
            foreach (JToken token in tokens)
            {
                SearchResults.Add(ParseJsonToken(token));
            }
        }

        public async Task GetAuthorBooksAsync(string author)
        {
            AuthorBookItems.Clear();
            string url = $"{_googleApiSettings.GoogleBooksURL}?q=inauthor:{author}";
            IList<JToken> tokens = await GetBookItemsAsJToken(url);
            foreach (JToken token in tokens)
            {
                AuthorBookItems.Add(ParseJsonToken(token));
            }
        }

        private async Task<IList<JToken>> GetBookItemsAsJToken(string url)
        {
            var uri = new Uri(url);
            var response = await _httpClient.GetAsync(uri);
            string textResult = await response.Content.ReadAsStringAsync();
            JObject bookSearch = JObject.Parse(textResult);
            IList<JToken> tokens = new List<JToken>();
            try
            {
                tokens = bookSearch["items"].Children().ToList();
            }
            catch (NullReferenceException error)
            {
                Console.WriteLine(error);
            }
            return tokens;

        }

        private Book ParseJsonToken(JToken jToken)
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
