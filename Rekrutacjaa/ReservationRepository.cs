using Rekrutacjaa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rekrutacjaa
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(LibraryDBContext context) : base(context) { }

        public List<Reservation> GetReservations(Guid id)
        {
            Expression<Func<Reservation, bool>> expressionPredicate = x => x.BookId == id;
            return (List<Reservation>)Find(expressionPredicate);

        }


    }
}
