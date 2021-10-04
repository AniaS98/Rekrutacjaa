using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rekrutacjaa.Models
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public bool IsAvailable  { get; set; }

    }
}
