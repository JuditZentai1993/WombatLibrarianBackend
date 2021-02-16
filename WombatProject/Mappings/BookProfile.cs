using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WombatLibrarianApi.Models;

namespace WombatLibrarianApi.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookItem, Book>()
                .ForMember(x => x.Title, opt => opt.MapFrom(m => m.volumeInfo.title))
                .ForMember(x => x.Subtitle, opt => opt.MapFrom(m => m.volumeInfo.subtitle))
                .ForMember(x => x.Thumbnail, opt => opt.MapFrom(m => m.volumeInfo.imageLinks.thumbnail))
                .ForMember(x => x.Description, opt => opt.MapFrom(m => m.volumeInfo.description))
                .ForMember(x => x.PageCount, opt => opt.MapFrom(m => m.volumeInfo.pageCount))
                .ForMember(x => x.Rating, opt => opt.MapFrom(m => m.volumeInfo.averageRating))
                .ForMember(x => x.RatingCount, opt => opt.MapFrom(m => m.volumeInfo.ratingsCount))
                .ForMember(x => x.Language, opt => opt.MapFrom(m => m.volumeInfo.language))
                .ForMember(x => x.MaturityRating, opt => opt.MapFrom(m => m.volumeInfo.maturityRating))
                .ForMember(x => x.Published, opt => opt.MapFrom(m => m.volumeInfo.publishedDate))
                .ForMember(x => x.Publisher, opt => opt.MapFrom(m => m.volumeInfo.publisher))
                .ForMember(x => x.Authors, opt => opt.MapFrom(src => ConvertAuthors(src.volumeInfo.authors)))
                .ForMember(x => x.Categories, opt => opt.MapFrom(src => ConvertCategories(src.volumeInfo.categories)));
        }

        private List<Author> ConvertAuthors(IEnumerable<string> source)
        {
            if (source == null)
            {
                return null;
            }
            var authors = new List<Author>();
            foreach (var author in source)
            {
                authors.Add(new Author { Name = author });
            }
            return authors;
        }

        private List<Category> ConvertCategories(IEnumerable<string> source)
        {
            if (source == null)
            {
                return null;
            }
            var categories = new List<Category>();
            foreach (var category in source)
            {
                categories.Add(new Category { Name = category });
            }
            return categories;
        }
    }
}
