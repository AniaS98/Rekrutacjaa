using Rekrutacjaa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rekrutacjaa
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(LibraryDBContext context) : base(context) { }

        public List<Book> GetBooks()
        {
            return (List<Book>)this.GetAll();
        }

        public Book GetBook(Guid id)
        {
            Expression<Func<Book, bool>> expressionPredicate = x => x.BookId == id;
            return Find(expressionPredicate)[0];
        }

    }
}
