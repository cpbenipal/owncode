namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbchange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.brJobs", "ManufactureID", "dbo.tblVehicleMakes");
            DropIndex("dbo.brJobs", new[] { "ManufactureID" });
            AddColumn("dbo.brJobs", "VehicleID", c => c.Int(nullable: false));
            AddColumn("dbo.brJobs", "BrVehicle_Id", c => c.Int());
            AddColumn("dbo.brVehicle", "TenantId", c => c.Int(nullable: false));
            CreateIndex("dbo.brJobs", "BrVehicle_Id");
            AddForeignKey("dbo.brJobs", "BrVehicle_Id", "dbo.brVehicle", "Id");
            DropColumn("dbo.brJobs", "ManufactureID");
            DropColumn("dbo.brJobs", "ModelID");
            DropColumn("dbo.brJobs", "RegNo");
            DropColumn("dbo.brJobs", "VinNumber");
            DropColumn("dbo.brJobs", "Colour");
            DropColumn("dbo.brJobs", "Year");
            DropColumn("dbo.brJobs", "UnderWaranty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.brJobs", "UnderWaranty", c => c.String());
            AddColumn("dbo.brJobs", "Year", c => c.String());
            AddColumn("dbo.brJobs", "Colour", c => c.String());
            AddColumn("dbo.brJobs", "VinNumber", c => c.String());
            AddColumn("dbo.brJobs", "RegNo", c => c.String());
            AddColumn("dbo.brJobs", "ModelID", c => c.Int(nullable: false));
            AddColumn("dbo.brJobs", "ManufactureID", c => c.Int(nullable: false));
            DropForeignKey("dbo.brJobs", "BrVehicle_Id", "dbo.brVehicle");
            DropIndex("dbo.brJobs", new[] { "BrVehicle_Id" });
            DropColumn("dbo.brVehicle", "TenantId");
            DropColumn("dbo.brJobs", "BrVehicle_Id");
            DropColumn("dbo.brJobs", "VehicleID");
            CreateIndex("dbo.brJobs", "ManufactureID");
            AddForeignKey("dbo.brJobs", "ManufactureID", "dbo.tblVehicleMakes", "Id", cascadeDelete: true);
        }
    }
}
