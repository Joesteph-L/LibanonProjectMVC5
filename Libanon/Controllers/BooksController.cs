using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Libanon.ConnectConfig;
using Libanon.Models;
using Libanon.Controllers;

namespace Libanon.Controllers
{
    public class BooksController : Controller
    {
        private LibanonDbContext db = new LibanonDbContext();

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
            var q = from temp in db.Images where temp.Book.ISBN == ISBN select temp;
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
            return View(db.Books.ToList());
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
        public ActionResult Create(Book book)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];

            book.Image = new Image
            {
                ImageName = "Picture - " + book.Title + "Author - " + book.Author,
                ImageBinary = ConvertToBytes(file),
                Type = "Book",
                Book = book
            };

            book.State = "Awaiting";
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
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
        public ActionResult Edit(Book book)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            var newBook = book;

            var newImage = GetImageByISBN(book.ISBN);
            newImage.ImageBinary = ConvertToBytes(file);
                
            
            if (ModelState.IsValid)
            {
                db.Entry(newBook).State = EntityState.Modified;
                db.Entry(newImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
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

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
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
