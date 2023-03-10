using System;
using System.Collections.Generic;

namespace Libanon.Models
{
    public class Book
    {
        //Properties of a book
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Language { get; set; }
        public string State { get; set; }

        //Reference Navigation Property
        public virtual ICollection<Image> ImagesList { get; set; }
        public Book()
        {
            ImagesList = new List<Image>();
        }
    }
}