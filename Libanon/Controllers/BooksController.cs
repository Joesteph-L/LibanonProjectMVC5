using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Libanon.Models;
using Libanon.Controllers;
using Libanon.Repository;

namespace Libanon.Controllers
{
    public class BooksController : Controller
    {
        private LibanonDbContext db = new LibanonDbContext();
        readonly IBookRepository bookRepository;
        public BooksController(IBookRepository BookRepository)
        {
            bookRepository = BookRepository;
        }

        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        public Image GetImageByISBN(int ISBN)
        {
            //var q = from temp in db.Images where temp.Book.ISBN == ISBN select temp;
            var q = db.Images.Where(i => i.Book.ISBN == ISBN);
            return q.FirstOrDefault();
        }

        public byte[] GetImageFromDataBase(int Id)
        {
            //var q = from temp in db.Images where temp.Book.ISBN == Id select temp.ImageBinary;
            var q = GetImageByISBN(Id).ImageBinary;
            byte[] cover = q;
            return cover;
        }

        

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        
        public ActionResult Index()
        {
            List<Book> ListBooks = bookRepository.GetAll().ToList();
            return View(ListBooks);
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book newBook)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];

            newBook.Image = new Image
            {
                ImageName = "Picture - " + newBook.Title + "Author - " + newBook.Author,
                ImageBinary = ConvertToBytes(file),
                Type = "Book",
                Book = newBook
            };
            var book = new Book();
            if (ModelState.IsValid)
            {
                book = bookRepository.AddNew(newBook);
            }
            else
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book tagetBook)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            

            var newImage = GetImageByISBN(tagetBook.ISBN);
            newImage.ImageBinary = ConvertToBytes(file);

            var book = new Book();
            if (ModelState.IsValid)
            {
                db.Entry(newImage).State = EntityState.Modified;
                db.SaveChanges();
                bookRepository.Update(tagetBook);
            }
            else
            {
                return HttpNotFound();
            }

            return View(book);
        }

       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!bookRepository.Delete(id))
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
