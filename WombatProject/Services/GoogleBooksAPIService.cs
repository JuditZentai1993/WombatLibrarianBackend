using Microsoft.EntityFrameworkCore;
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
    public class GoogleBooksAPIService : IBookAPIService
    {
        private readonly IConfiguration _configuration;
        public WombatBooksContext Context { get; }
        public List<Book> AuthorBookItems { get; set; } = new List<Book>();
        public List<Book> SearchResults { get; set; } = new List<Book>();

        public GoogleBooksAPIService(IConfiguration configuration, WombatBooksContext context)
        {
            _configuration = configuration;
            Context = context;
        }

        public async Task GetSearchResults(string searchTerm)
        {
            SearchResults.Clear();
            string url = $"{_configuration["GBooksURL"]}?q={searchTerm}&maxResults=40";
            IList<JToken> tokens = await GetBookItemsAsJToken(url);
            foreach (JToken token in tokens)
            {
                SearchResults.Add(parseJsonToken(token));
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

        public async Task<IEnumerable<object>> GetBooksFromBookshelf()
        {
            var bookIds = Context.Bookshelves.Select(book => book.BookId).ToList();
            return await Context.Books
                .Where(book => bookIds.Contains(book.Id))
                .Include(bookShelfItem => bookShelfItem.Authors)
                .Include(bookShelfItem => bookShelfItem.Categories)
                .Join(Context.Bookshelves,
                book => book.Id,
                bookshelf => bookshelf.BookId,
                (book, bookshelf) => new 
                        {
                            Id = book.Id,
                            Title = book.Title,
                            Subtitle = book.Subtitle,
                            Thumbnail = book.Thumbnail,
                            Description = book.Description,
                            PageCount = book.PageCount,
                            Rating = book.Rating,
                            RatingCount = book.RatingCount,
                            Language = book.Language,
                            MaturityRating = book.MaturityRating,
                            Published = book.Published,
                            Publisher = book.Publisher,
                            BookshelfId = bookshelf.Id
                        })
                .ToListAsync();
        }

        public async Task<Bookshelf> AddBookToBookshelf(Book book)
        {
            Context.Authors.AddRange(book.Authors);
            Context.Categories.AddRange(book.Categories);
            Context.Books.Add(book);
            Bookshelf bookshelf = new Bookshelf() { BookId = book.Id };
            Context.Bookshelves.Add(bookshelf);
            await Context.SaveChangesAsync();
            return bookshelf;
        }

        private async Task<IList<JToken>> GetBookItemsAsJToken(string url)
        {
            var client = new HttpClient();
            var uri = new Uri(url);
            var response = await client.GetAsync(uri);
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

        public async Task<IEnumerable<object>> GetBooksFromWishlist()
        {
            var bookIds = Context.Wishlists.Select(book => book.BookId).ToList();
            return await Context.Books
                .Where(book => bookIds.Contains(book.Id))
                .Include(wishlistItem => wishlistItem.Authors)
                .Include(wishlistItem => wishlistItem.Categories)
                .Join(Context.Wishlists,
                book => book.Id,
                wishlist => wishlist.BookId,
                (book, wishlist) => new
                {
                    Id = book.Id,
                    Title = book.Title,
                    Subtitle = book.Subtitle,
                    Thumbnail = book.Thumbnail,
                    Description = book.Description,
                    PageCount = book.PageCount,
                    Rating = book.Rating,
                    RatingCount = book.RatingCount,
                    Language = book.Language,
                    MaturityRating = book.MaturityRating,
                    Published = book.Published,
                    Publisher = book.Publisher,
                    WishlistId = wishlist.Id
                })
                .ToListAsync();
        }

        public Task<Bookshelf> AddBookToWishlist(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
