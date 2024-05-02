using Service.Database.Models;
using System.Data.Entity;

namespace Service.Database.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("MySqlConnection") { }

        // tabele u bazi
        public DbSet<Vozac> Vozaci { get; set; }

        public DbSet<Linija> Linije { get; set; }
    }
}
