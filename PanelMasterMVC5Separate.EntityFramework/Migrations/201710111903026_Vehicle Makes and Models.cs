namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class VehicleMakesandModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblVehicleMakes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 500),
                        LogoPicture = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
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
                    { "DynamicFilter_VehicleMake_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblVehicleModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleMakeID = c.Int(nullable: false),
                        Model = c.String(nullable: false),
                        MMCode = c.String(nullable: false),
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
                    { "DynamicFilter_VehicleModels_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblVehicleMakes", t => t.VehicleMakeID, cascadeDelete: true)
                .Index(t => t.VehicleMakeID);
            
            CreateTable(
                "dbo.tblVehiclemodelLogos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bytes = c.Binary(nullable: false),
                        VehicleMakeID = c.Int(nullable: false),
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
                    { "DynamicFilter_VehicleModelLogos_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblVehicleMakes", t => t.VehicleMakeID, cascadeDelete: true)
                .Index(t => t.VehicleMakeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblVehiclemodelLogos", "VehicleMakeID", "dbo.tblVehicleMakes");
            DropForeignKey("dbo.tblVehicleModels", "VehicleMakeID", "dbo.tblVehicleMakes");
            DropIndex("dbo.tblVehiclemodelLogos", new[] { "VehicleMakeID" });
            DropIndex("dbo.tblVehicleModels", new[] { "VehicleMakeID" });
            DropTable("dbo.tblVehiclemodelLogos",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehicleModelLogos_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblVehicleModels",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehicleModels_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblVehicleMakes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VehicleMake_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
