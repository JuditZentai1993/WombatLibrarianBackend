﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WombatLibrarianApi.Models;

namespace WombatLibrarianApi.Services
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooksFromBookshelfAsync();
        Task<Bookshelf> GetBookshelfItemByIdAsync(int id);
        Task<Bookshelf> AddBookToBookshelfAsync(Book book);
        Task RemoveBookFromBookshelfByIdAsync(string bookid);
        Task<IEnumerable<Book>> GetBooksFromWishlistAsync();
        Task<Wishlist> GetWishlistItemByIdAsync(int id);
        Task<Wishlist> AddBookToWishlistAsync(Book book);
        Task RemoveBookFromWishlistByIdAsync(string bookid);
    }
}
