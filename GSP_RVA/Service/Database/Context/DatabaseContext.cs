using Service.Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
