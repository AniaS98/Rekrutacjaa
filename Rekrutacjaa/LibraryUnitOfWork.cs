using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Rekrutacjaa.Models;

namespace Rekrutacjaa
{
    public class LibraryUnitOfWork : IUnitOfWork
    {
        private readonly LibraryDBContext context;
        public IBookRepository BookRepository { get; }
        public IUserRepository UserRepository { get; }
        public IReservationRepository ReservationRepository { get; }

        public LibraryUnitOfWork(LibraryDBContext context)
        {
            this.context = context;
            this.BookRepository = new BookRepository(this.context);
            this.ReservationRepository = new ReservationRepository(this.context);
            this.UserRepository = new UserRepository(this.context);
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
        public void RejectChanges()
        {
            foreach (var entry in context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }




    }
}
