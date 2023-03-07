
using Libanon.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Libanon.Repository
{
    public class BookRepository : IBookRepository
    {
        LibanonDbContext _DbContext;

        public BookRepository()
        {
            _DbContext = new LibanonDbContext();
        }

        public Book AddNew(Book newBook)
        {
            if (newBook == null)
            {
                throw new Exception("Input book cannot be null!!");
            }
            else
            {
                if (newBook.State == null)
                {
                    newBook.State = "Awaiting";
                }
                _DbContext.Books.Add(newBook);
                _DbContext.SaveChanges();
            }

            return newBook;
        }

        public Book Update(Book book)
        {
            if (book == null)
            {
                throw new Exception("Update book cannot be null!!");
            }
            else
            {
                _DbContext.Entry(book).State = EntityState.Modified;
                _DbContext.SaveChanges();
            }

            return book;
        }

        public IEnumerable<Book> GetAll()
        {
            List<Book> ListBooks = _DbContext.Books.ToList();
            return ListBooks;
        }

        public Book GetDetail(int? id)
        {
            if(id == null) {
                throw new Exception("Id book cannot be null!!");
            }

            Book book = _DbContext.Books.Find(id);
            return book;
        }

        public List<Book> Search(string keyword)
        {
            var book = new List<Book>();
            if (keyword == null) 
            { 
                throw new Exception("Keyword cannot be null!!");
            }
            else
            {
                book = _DbContext.Books.Where(b => b.Title == keyword).ToList();
            }
            return book; 
        }

        public bool Delete(int? id)
        {
            if (id == null)
            {
                throw new Exception("Id book cannot be null!!");
            }

            bool result = false;
            Book book = _DbContext.Books.Find(id);
            _DbContext.Books.Remove(book);
            _DbContext.SaveChanges();

            if (_DbContext.Entry(book).State == EntityState.Deleted)
            {
                result = true;
            }
            
            return result;
        }
    }
}