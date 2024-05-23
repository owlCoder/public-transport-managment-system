using MySql.Data.EntityFramework;
using Service.Database.Models;
using System.Data.Entity;

namespace Service.Database.Context
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("MySqlConnection") { }

        public DbSet<Vozac> Vozaci { get; set; }

        public DbSet<Linija> Linije { get; set; }

        public DbSet<Autobus> Autobusi { get; set; }


        // TODO: add more tables
    }
}