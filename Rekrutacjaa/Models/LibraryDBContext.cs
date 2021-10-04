using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Rekrutacjaa.Models
{
    public class LibraryDBContext : DbContext
    {
        public DbSet<User> user { get; set; }
        public DbSet<Book> book { get; set; }
        public DbSet<Reservation> reservation { get; set; }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EFTrail;Trusted_Connection=True;");
        }
        */
    }
}
