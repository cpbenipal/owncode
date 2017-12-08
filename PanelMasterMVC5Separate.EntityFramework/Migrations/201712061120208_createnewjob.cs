namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class createnewjob : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblPaintType", t => t.PaintTypes_Id)
                .ForeignKey("dbo.tblVehicleMakes", t => t.VehicleMake_Id)
                .ForeignKey("dbo.tblVehicleModels", t => t.VehicleModels_Id)
                .Index(t => t.PaintTypes_Id)
                .Index(t => t.VehicleMake_Id)
                .Index(t => t.VehicleModels_Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblBrokerMaster", t => t.BrokerMasters_Id)
                .ForeignKey("dbo.tblInsurerMaster", t => t.InsurerMasters_Id)
                .Index(t => t.BrokerMasters_Id)
                .Index(t => t.InsurerMasters_Id);
            
            AddColumn("dbo.brJobs", "DamangeReason", c => c.String());
            AddColumn("dbo.brJobs", "BranchEntryMethod", c => c.String());
            AddColumn("dbo.brJobs", "IsUnrelatedDamangeReason", c => c.Boolean(nullable: false));
            AddColumn("dbo.brJobs", "CurrentKMs", c => c.String());
            AddColumn("dbo.brJobs", "OtherInformation", c => c.String());
            AddColumn("dbo.brClient", "IdNumber", c => c.String());
            AddColumn("dbo.brClient", "OtherInformation", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.brINS", "InsurerMasters_Id", "dbo.tblInsurerMaster");
            DropForeignKey("dbo.brINS", "BrokerMasters_Id", "dbo.tblBrokerMaster");
            DropForeignKey("dbo.brVehicle", "VehicleModels_Id", "dbo.tblVehicleModels");
            DropForeignKey("dbo.brVehicle", "VehicleMake_Id", "dbo.tblVehicleMakes");
            DropForeignKey("dbo.brVehicle", "PaintTypes_Id", "dbo.tblPaintType");
            DropIndex("dbo.brINS", new[] { "InsurerMasters_Id" });
            DropIndex("dbo.brINS", new[] { "BrokerMasters_Id" });
            DropIndex("dbo.brVehicle", new[] { "VehicleModels_Id" });
            DropIndex("dbo.brVehicle", new[] { "VehicleMake_Id" });
            DropIndex("dbo.brVehicle", new[] { "PaintTypes_Id" });
            DropColumn("dbo.brClient", "OtherInformation");
            DropColumn("dbo.brClient", "IdNumber");
            DropColumn("dbo.brJobs", "OtherInformation");
            DropColumn("dbo.brJobs", "CurrentKMs");
            DropColumn("dbo.brJobs", "IsUnrelatedDamangeReason");
            DropColumn("dbo.brJobs", "BranchEntryMethod");
            DropColumn("dbo.brJobs", "DamangeReason");
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
