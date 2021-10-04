using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rekrutacjaa.Models
{
    public class Reservation
    {
        public Guid ReservationId { get; set; }
        public DateTime ReservationDate { get; set; }
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }

    }
}
