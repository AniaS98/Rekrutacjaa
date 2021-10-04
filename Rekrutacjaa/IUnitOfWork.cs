using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rekrutacjaa
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository BookRepository { get; }
        IUserRepository UserRepository { get; }
        IReservationRepository ReservationRepository { get; }
        void Commit();
        void RejectChanges();

    }
}
