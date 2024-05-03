using MySql.Data.EntityFramework;
using Service.Database.Models;
using System.Data.Entity;

namespace Service.Database.Context
{
    /// <summary>
    /// Represents the database context for interacting with the MySQL database using Entity Framework.
    /// </summary>
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext"/> class.
        /// </summary>
        public DatabaseContext() : base("MySqlConnection") { }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{Vozac}"/> for the Vozac entity set (table).
        /// </summary>
        public DbSet<Vozac> Vozaci { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DbSet{Linija}"/> for the Linija entity set (table).
        /// </summary>
        public DbSet<Linija> Linije { get; set; }

        /// <summary>
        /// Busevi
        /// </summary>
        // TODO: add more tables
    }
}