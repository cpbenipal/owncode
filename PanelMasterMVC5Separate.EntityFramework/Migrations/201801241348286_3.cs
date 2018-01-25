namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.brVehicle", "VehicleCode", c => c.Int(nullable: false));
            AddColumn("dbo.brVehicle", "MM_Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.brVehicle", "MM_Code");
            DropColumn("dbo.brVehicle", "VehicleCode");
        }
    }
}
