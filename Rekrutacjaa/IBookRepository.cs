using Rekrutacjaa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rekrutacjaa
{
    public interface IBookRepository : IRepository<Book>
    {
        List<Book> GetBooks();
        Book GetBook(Guid id);

    }
}
