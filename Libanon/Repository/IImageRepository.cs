using Libanon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libanon.Repository
{
    public interface IImageRepository
    {
        Image AddNew(Image newImage);
        IEnumerable<Image> GetAll();
        Image GetImage(int id);
        Image Delete(int id);
    }
}
