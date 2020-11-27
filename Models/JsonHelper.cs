using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WombatLibrarianApi.Models
{
    public class JsonHelper
    {
        public static Book parseJsonToken(JToken jToken)
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
