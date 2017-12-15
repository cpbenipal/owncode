namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewQuote1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.brVehicle", "TenantId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.brVehicle", "TenantId", c => c.Int(nullable: false));
        }
    }
}
