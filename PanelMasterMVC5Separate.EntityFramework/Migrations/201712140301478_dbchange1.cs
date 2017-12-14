namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbchange1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.brJobs", "BrVehicle_Id", "dbo.brVehicle");
            DropIndex("dbo.brJobs", new[] { "BrVehicle_Id" });
            DropColumn("dbo.brJobs", "VehicleID");
            RenameColumn(table: "dbo.brJobs", name: "BrVehicle_Id", newName: "VehicleID");
            AlterColumn("dbo.brJobs", "VehicleID", c => c.Int());
            CreateIndex("dbo.brJobs", "VehicleID");
            AddForeignKey("dbo.brJobs", "VehicleID", "dbo.brVehicle", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.brJobs", "VehicleID", "dbo.brVehicle");
            DropIndex("dbo.brJobs", new[] { "VehicleID" });
            AlterColumn("dbo.brJobs", "VehicleID", c => c.Int());
            RenameColumn(table: "dbo.brJobs", name: "VehicleID", newName: "BrVehicle_Id");
            AddColumn("dbo.brJobs", "VehicleID", c => c.Int());
            CreateIndex("dbo.brJobs", "BrVehicle_Id");
            AddForeignKey("dbo.brJobs", "BrVehicle_Id", "dbo.brVehicle", "Id");
        }
    }
}
