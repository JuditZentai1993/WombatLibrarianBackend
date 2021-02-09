using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WombatLibrarianApi.Models;
using WombatLibrarianApi.Settings;

namespace WombatLibrarianApi.Services
{
    public class GoogleBooksAPIService : IBookAPIService
    {
        private readonly IHttpClientFactory _clientFactory;

        protected readonly GoogleApiSettings _googleApiSettings;

        public GoogleBooksAPIService(IOptions<GoogleApiSettings> settings, IHttpClientFactory clientFactory)
        {
            _googleApiSettings = settings.Value;
            _clientFactory = clientFactory;
        }

        public async Task<IList<BookItem>> GetSearchResultsAsync(string searchTerm)
        {
            string url = $"{_googleApiSettings.GoogleBooksURL}?q={searchTerm}&maxResults=40";
            return await SerializeResultsFromGoogleBooksApiAsync(url);
        }

        public async Task<IList<BookItem>> GetAuthorBooksAsync(string author)
        {
            string url = $"{_googleApiSettings.GoogleBooksURL}?q=inauthor:{author}";
            return await SerializeResultsFromGoogleBooksApiAsync(url);
        }

        public async Task<IList<BookItem>> SerializeResultsFromGoogleBooksApiAsync (string url)
        {
            var client = _clientFactory.CreateClient();
            var uri = new Uri(url);
            var response = await client.SendAsync(new HttpRequestMessage { RequestUri = uri, Method = HttpMethod.Get });
            string textResult = await response.Content.ReadAsStringAsync();
            GoogleBookApiSearchResponse myDeserializedClass = JsonConvert.DeserializeObject
                <GoogleBookApiSearchResponse>(textResult);

            return myDeserializedClass.items;
        }
    }
}
