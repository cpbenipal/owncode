namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.brVehicle", "UnderWaranty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.brVehicle", "UnderWaranty", c => c.Boolean(nullable: false));
        }
    }
}
