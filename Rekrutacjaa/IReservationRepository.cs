using Rekrutacjaa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rekrutacjaa
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        List<Reservation> GetReservations(Guid id);
    }
}
