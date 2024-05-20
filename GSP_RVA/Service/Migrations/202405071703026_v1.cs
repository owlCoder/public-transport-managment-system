namespace Service.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Autobusi",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Oznaka = c.String(nullable: false, maxLength: 8, storeType: "nvarchar"),
                    IdLinije = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Linije",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Oznaka = c.String(nullable: false, maxLength: 8, storeType: "nvarchar"),
                    Polaziste = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    Odrediste = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Vozaci",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Username = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    Password = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    Ime = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    Prezime = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    Role = c.String(nullable: false, unicode: false),
                    Oznaka = c.String(nullable: false, maxLength: 8, storeType: "nvarchar"),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.Vozaci");
            DropTable("dbo.Linije");
            DropTable("dbo.Autobusi");
        }
    }
}
