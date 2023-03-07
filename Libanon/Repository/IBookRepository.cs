using Libanon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libanon.Repository
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book GetDetail(int? id);
        Book AddNew(Book newBook);
        Book Update(Book newBook);
        bool Delete(int? id);
        List<Book> Search(string keyword);
    }
}
