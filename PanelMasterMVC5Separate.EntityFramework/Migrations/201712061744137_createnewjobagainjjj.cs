namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class createnewjobagainjjj : DbMigration
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
            DropTable("dbo.brVehicle",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BrVehicle_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblPaintType",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PaintTypes_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.brINS",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehicleInsurance_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.brINS",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InsurerID = c.Int(nullable: false),
                        BrokerID = c.Int(nullable: false),
                        ClaimAdministrator = c.String(),
                        PolicyNumber = c.String(),
                        OtherInformation = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        BrokerMasters_Id = c.Int(),
                        InsurerMasters_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehicleInsurance_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblPaintType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaintType = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PaintTypes_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.brVehicle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MakeId = c.Int(nullable: false),
                        ModelId = c.Int(nullable: false),
                        Color = c.String(),
                        PaintPaintTypeId = c.Int(nullable: false),
                        Year = c.String(maxLength: 4),
                        RegistrationNumber = c.String(),
                        VinNumber = c.String(),
                        UnderWaranty = c.Boolean(nullable: false),
                        IsSpecialisedType = c.Boolean(nullable: false),
                        IsLuxury = c.Boolean(nullable: false),
                        OtherInformation = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        PaintTypes_Id = c.Int(),
                        VehicleMake_Id = c.Int(),
                        VehicleModels_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BrVehicle_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
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
