﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WombatLibrarianApi.Models
{
    public class Wishlist
    {
        [Key]
        public int Id { get; set; }

        public List<Book> Books { get; set; }
    }
}
