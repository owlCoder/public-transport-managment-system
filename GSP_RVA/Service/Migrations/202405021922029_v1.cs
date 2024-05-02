namespace Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Linijas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Vozac_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vozacs", t => t.Vozac_Id)
                .Index(t => t.Vozac_Id);
            
            CreateTable(
                "dbo.Vozacs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Ime = c.String(nullable: false, maxLength: 50),
                        Prezime = c.String(nullable: false, maxLength: 50),
                        Role = c.Int(nullable: false),
                        Oznaka = c.String(nullable: false, maxLength: 8),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Username, unique: true, name: "Username");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Linijas", "Vozac_Id", "dbo.Vozacs");
            DropIndex("dbo.Vozacs", "Username");
            DropIndex("dbo.Linijas", new[] { "Vozac_Id" });
            DropTable("dbo.Vozacs");
            DropTable("dbo.Linijas");
        }
    }
}
