using MySql.Data.EntityFramework;
using Service.Database.Models;
using System.Data.Entity;
using System.Linq;

namespace Service.Database.Context
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("MySqlConnection") { }

        public DbSet<Vozac> Vozaci { get; set; }
        public DbSet<Linija> Linije { get; set; }
        public DbSet<Autobus> Autobusi { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure any additional model relationships or constraints here
            modelBuilder.Entity<Vozac>()
                .HasMany(v => v.Linije)
                .WithMany(l => l.Vozaci)
                .Map(m =>
                {
                    m.ToTable("VozacLinija");
                    m.MapLeftKey("VozacId");
                    m.MapRightKey("LinijaId");
                });

            modelBuilder.Entity<Linija>()
                .HasMany(l => l.Autobusi)
                .WithOptional(a => a.Linija) // WithOptional because IdLinije is nullable
                .HasForeignKey(a => a.IdLinije);

            modelBuilder.Entity<Autobus>()
                .HasOptional(a => a.Linija) // HasOptional because IdLinije is nullable
                .WithMany(l => l.Autobusi)
                .HasForeignKey(a => a.IdLinije)
                .WillCascadeOnDelete(false);
        }

        public IQueryable<Vozac> GetVozaciWithRelatedData()
        {
            return Vozaci.Include(v => v.Linije.Select(l => l.Autobusi));
        }

        public IQueryable<Linija> GetLinijeWithRelatedData()
        {
            return Linije.Include(l => l.Vozaci).Include(l => l.Autobusi);
        }

        public IQueryable<Autobus> GetAutobusiWithRelatedData()
        {
            return Autobusi.Include(a => a.Linija.Vozaci);
        }
    }
}
