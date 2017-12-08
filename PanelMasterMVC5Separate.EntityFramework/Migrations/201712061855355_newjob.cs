namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class newjob : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.brVehicle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Color = c.String(),
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
                        PaintTypesId = c.Int(),
                        VehicleMakeId = c.Int(),
                        VehicleModelId = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BrVehicle_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblPaintType", t => t.PaintTypesId)
                .ForeignKey("dbo.tblVehicleMakes", t => t.VehicleMakeId)
                .ForeignKey("dbo.tblVehicleModels", t => t.VehicleModelId)
                .Index(t => t.PaintTypesId)
                .Index(t => t.VehicleMakeId)
                .Index(t => t.VehicleModelId);
            
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
                "dbo.brINS",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                        BrokerMastersId = c.Int(),
                        InsurerMastersId = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehicleInsurance_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblBrokerMaster", t => t.BrokerMastersId)
                .ForeignKey("dbo.tblInsurerMaster", t => t.InsurerMastersId)
                .Index(t => t.BrokerMastersId)
                .Index(t => t.InsurerMastersId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.brINS", "InsurerMastersId", "dbo.tblInsurerMaster");
            DropForeignKey("dbo.brINS", "BrokerMastersId", "dbo.tblBrokerMaster");
            DropForeignKey("dbo.brVehicle", "VehicleModelId", "dbo.tblVehicleModels");
            DropForeignKey("dbo.brVehicle", "VehicleMakeId", "dbo.tblVehicleMakes");
            DropForeignKey("dbo.brVehicle", "PaintTypesId", "dbo.tblPaintType");
            DropIndex("dbo.brINS", new[] { "InsurerMastersId" });
            DropIndex("dbo.brINS", new[] { "BrokerMastersId" });
            DropIndex("dbo.brVehicle", new[] { "VehicleModelId" });
            DropIndex("dbo.brVehicle", new[] { "VehicleMakeId" });
            DropIndex("dbo.brVehicle", new[] { "PaintTypesId" });
            DropTable("dbo.brINS",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehicleInsurance_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblPaintType",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PaintTypes_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.brVehicle",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BrVehicle_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
