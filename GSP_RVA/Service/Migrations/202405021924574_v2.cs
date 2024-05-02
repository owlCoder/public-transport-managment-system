namespace Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Linijas", "Vozac_Id", "dbo.Vozacs");
            DropIndex("dbo.Linijas", new[] { "Vozac_Id" });
            DropColumn("dbo.Linijas", "Vozac_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Linijas", "Vozac_Id", c => c.Int());
            CreateIndex("dbo.Linijas", "Vozac_Id");
            AddForeignKey("dbo.Linijas", "Vozac_Id", "dbo.Vozacs", "Id");
        }
    }
}
