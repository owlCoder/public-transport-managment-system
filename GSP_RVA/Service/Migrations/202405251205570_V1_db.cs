namespace Service.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class V1_db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Autobusi",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Oznaka = c.String(nullable: false, maxLength: 8, storeType: "nvarchar"),
                    IdLinije = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Linije", t => t.IdLinije)
                .Index(t => t.IdLinije);

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

            CreateTable(
                "dbo.VozacLinija",
                c => new
                {
                    VozacId = c.Int(nullable: false),
                    LinijaId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.VozacId, t.LinijaId })
                .ForeignKey("dbo.Vozaci", t => t.VozacId, cascadeDelete: true)
                .ForeignKey("dbo.Linije", t => t.LinijaId, cascadeDelete: true)
                .Index(t => t.VozacId)
                .Index(t => t.LinijaId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Autobusi", "IdLinije", "dbo.Linije");
            DropForeignKey("dbo.VozacLinija", "LinijaId", "dbo.Linije");
            DropForeignKey("dbo.VozacLinija", "VozacId", "dbo.Vozaci");
            DropIndex("dbo.VozacLinija", new[] { "LinijaId" });
            DropIndex("dbo.VozacLinija", new[] { "VozacId" });
            DropIndex("dbo.Autobusi", new[] { "IdLinije" });
            DropTable("dbo.VozacLinija");
            DropTable("dbo.Vozaci");
            DropTable("dbo.Linije");
            DropTable("dbo.Autobusi");
        }
    }
}
