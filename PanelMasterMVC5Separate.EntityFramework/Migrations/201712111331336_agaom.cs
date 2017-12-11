namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agaom : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.brVehicle", "PaintTypes_Id", "dbo.tblPaintType");
            DropForeignKey("dbo.brVehicle", "VehicleMake_Id", "dbo.tblVehicleMakes");
            DropForeignKey("dbo.brVehicle", "VehicleModels_Id", "dbo.tblVehicleModels");
            DropForeignKey("dbo.brINS", "BrokerMasters_Id", "dbo.tblBrokerMaster");
            DropForeignKey("dbo.brINS", "InsurerMasters_Id", "dbo.tblInsurerMaster");
            DropIndex("dbo.brVehicle", new[] { "PaintTypes_Id" });
            DropIndex("dbo.brVehicle", new[] { "VehicleMake_Id" });
            DropIndex("dbo.brVehicle", new[] { "VehicleModels_Id" });
            DropIndex("dbo.brINS", new[] { "BrokerMasters_Id" });
            DropIndex("dbo.brINS", new[] { "InsurerMasters_Id" });
            RenameColumn(table: "dbo.brVehicle", name: "PaintTypes_Id", newName: "PaintTypeId");
            RenameColumn(table: "dbo.brVehicle", name: "VehicleMake_Id", newName: "MakeId");
            RenameColumn(table: "dbo.brVehicle", name: "VehicleModels_Id", newName: "ModelId");
            RenameColumn(table: "dbo.brINS", name: "BrokerMasters_Id", newName: "BrokerId");
            RenameColumn(table: "dbo.brINS", name: "InsurerMasters_Id", newName: "InsurerId");
            AlterColumn("dbo.brVehicle", "Color", c => c.String(nullable: false));
            AlterColumn("dbo.brVehicle", "PaintTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.brVehicle", "MakeId", c => c.Int(nullable: false));
            AlterColumn("dbo.brVehicle", "ModelId", c => c.Int(nullable: false));
            AlterColumn("dbo.brINS", "BrokerId", c => c.Int(nullable: false));
            AlterColumn("dbo.brINS", "InsurerId", c => c.Int(nullable: false));
            CreateIndex("dbo.brVehicle", "MakeId");
            CreateIndex("dbo.brVehicle", "ModelId");
            CreateIndex("dbo.brVehicle", "PaintTypeId");
            CreateIndex("dbo.brINS", "InsurerId");
            CreateIndex("dbo.brINS", "BrokerId");
            AddForeignKey("dbo.brVehicle", "PaintTypeId", "dbo.tblPaintType", "Id", cascadeDelete: true);
            AddForeignKey("dbo.brVehicle", "MakeId", "dbo.tblVehicleMakes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.brVehicle", "ModelId", "dbo.tblVehicleModels", "Id", cascadeDelete: false);
            AddForeignKey("dbo.brINS", "BrokerId", "dbo.tblBrokerMaster", "Id", cascadeDelete: true);
            AddForeignKey("dbo.brINS", "InsurerId", "dbo.tblInsurerMaster", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.brINS", "InsurerId", "dbo.tblInsurerMaster");
            DropForeignKey("dbo.brINS", "BrokerId", "dbo.tblBrokerMaster");
            DropForeignKey("dbo.brVehicle", "ModelId", "dbo.tblVehicleModels");
            DropForeignKey("dbo.brVehicle", "MakeId", "dbo.tblVehicleMakes");
            DropForeignKey("dbo.brVehicle", "PaintTypeId", "dbo.tblPaintType");
            DropIndex("dbo.brINS", new[] { "BrokerId" });
            DropIndex("dbo.brINS", new[] { "InsurerId" });
            DropIndex("dbo.brVehicle", new[] { "PaintTypeId" });
            DropIndex("dbo.brVehicle", new[] { "ModelId" });
            DropIndex("dbo.brVehicle", new[] { "MakeId" });
            AlterColumn("dbo.brINS", "InsurerId", c => c.Int());
            AlterColumn("dbo.brINS", "BrokerId", c => c.Int());
            AlterColumn("dbo.brVehicle", "ModelId", c => c.Int());
            AlterColumn("dbo.brVehicle", "MakeId", c => c.Int());
            AlterColumn("dbo.brVehicle", "PaintTypeId", c => c.Int());
            AlterColumn("dbo.brVehicle", "Color", c => c.String());
            RenameColumn(table: "dbo.brINS", name: "InsurerId", newName: "InsurerMasters_Id");
            RenameColumn(table: "dbo.brINS", name: "BrokerId", newName: "BrokerMasters_Id");
            RenameColumn(table: "dbo.brVehicle", name: "ModelId", newName: "VehicleModels_Id");
            RenameColumn(table: "dbo.brVehicle", name: "MakeId", newName: "VehicleMake_Id");
            RenameColumn(table: "dbo.brVehicle", name: "PaintTypeId", newName: "PaintTypes_Id");
            CreateIndex("dbo.brINS", "InsurerMasters_Id");
            CreateIndex("dbo.brINS", "BrokerMasters_Id");
            CreateIndex("dbo.brVehicle", "VehicleModels_Id");
            CreateIndex("dbo.brVehicle", "VehicleMake_Id");
            CreateIndex("dbo.brVehicle", "PaintTypes_Id");
            AddForeignKey("dbo.brINS", "InsurerMasters_Id", "dbo.tblInsurerMaster", "Id");
            AddForeignKey("dbo.brINS", "BrokerMasters_Id", "dbo.tblBrokerMaster", "Id");
            AddForeignKey("dbo.brVehicle", "VehicleModels_Id", "dbo.tblVehicleModels", "Id");
            AddForeignKey("dbo.brVehicle", "VehicleMake_Id", "dbo.tblVehicleMakes", "Id");
            AddForeignKey("dbo.brVehicle", "PaintTypes_Id", "dbo.tblPaintType", "Id");
        }
    }
}
