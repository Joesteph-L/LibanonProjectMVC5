using Libanon.Models;
using System.Collections.Generic;
using System.Web;

namespace Libanon.Repository
{
    public interface IImageRepository
    {
        Image AddNew(Image newImage);
        Image SetImage(HttpPostedFileBase file, Book newBook);
        IEnumerable<Image> GetAll();
        Image GetImage(int? i);
        Image Update(Image image, HttpPostedFileBase file);
        Image Delete(int id);
    }
}
