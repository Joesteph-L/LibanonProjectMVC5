using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Libanon.Models;
using Libanon.Repository;

namespace Libanon.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        readonly IBookRepository bookRepository;
        readonly IImageRepository imageRepository;
        public BooksController(IBookRepository bookRepository, IImageRepository imageRepository)
        {
            this.bookRepository = bookRepository;
            this.imageRepository = imageRepository;
        }

        public ActionResult RetrieveImage(int? id)
        {
            
            byte[] cover = imageRepository.GetImage(id).ImageBinary;
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<Book> ListBooks = bookRepository.GetAll().ToList();
            return View(ListBooks);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = bookRepository.GetDetail(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book newBook)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            var image = imageRepository.SetImage(file, newBook);
            newBook.ImagesList.Add(image);
            
            if (ModelState.IsValid)
            {
                bookRepository.AddNew(newBook);
            }
            else
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = bookRepository.GetDetail(id);
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

            
            if (ModelState.IsValid)
            {
                if(file.ContentLength > 0)
                {
                    var image = imageRepository.GetImage(tagetBook.ISBN);
                    imageRepository.Update(image, file);
                }
                
                bookRepository.Update(tagetBook);
            }
            else
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = bookRepository.GetDetail(id);
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

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
