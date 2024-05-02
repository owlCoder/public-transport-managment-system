namespace Service.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Linije",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
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
                    Role = c.Int(nullable: false),
                    Oznaka = c.String(nullable: false, maxLength: 8, storeType: "nvarchar"),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Username, unique: true, name: "Username");

        }

        public override void Down()
        {
            DropIndex("dbo.Vozaci", "Username");
            DropTable("dbo.Vozaci");
            DropTable("dbo.Linije");
        }
    }
}
