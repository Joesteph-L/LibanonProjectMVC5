using Libanon.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace Libanon.Repository
{
    public class ImageRepository:IImageRepository
    {
        LibanonDbContext _DbContext;

        public ImageRepository()
        {
            _DbContext = new LibanonDbContext();
        }

        private byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        public Image SetImage(HttpPostedFileBase file, Book newBook) 
        {
            var image = new Image();
            if(file == null)
            {
                image = null;
            }
            else
            {
                image = new Image
                {
                    ImageName = "Picture - " + newBook.Title + "Author - " + newBook.Author + file.FileName,
                    ImageBinary = ConvertToBytes(file),
                    Type = "Book",
                    BookISBN = newBook.ISBN
                };
            }
            return image;
        }

        public Image AddNew(Image newImage)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Image> GetAll()
        {
            throw new NotImplementedException();
        }

        public Image GetImage(int? ISBN)
        {
            if(ISBN == null)
            {
                return null;
            }
            var q = _DbContext.Images.Where(x => x.BookISBN == ISBN).FirstOrDefault();
            return q;
        }

        public Image Update(Image image, HttpPostedFileBase file)
        {
            if (image == null)
            {
                return image;
            }
            else
            {
                image.ImageBinary = ConvertToBytes(file);
                image.ImageName = "Picture - " + image.Book.Title + "Author - " + image.Book.Author + file.FileName;

                _DbContext.Entry(image).State = EntityState.Modified;
                _DbContext.SaveChanges();
            }

            return image;
        }

        public Image Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}