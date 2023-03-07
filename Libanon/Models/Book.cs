using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        public virtual Image Image { get; set; }
    }
}