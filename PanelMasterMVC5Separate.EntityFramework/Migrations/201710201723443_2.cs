namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.brJobs", "ModelID", "dbo.tblVehicleModel");
            DropForeignKey("dbo.tblVendors", "BankID", "dbo.tblBanks");
            DropForeignKey("dbo.tblVendors", "CurrencyID", "dbo.tblCurrency");
            DropIndex("dbo.brJobs", new[] { "ModelID" });
            DropIndex("dbo.tblVendors", new[] { "CurrencyID" });
            DropIndex("dbo.tblVendors", new[] { "BankID" });
            //DropTable("dbo.tblBroker",
            //    removedAnnotations: new Dictionary<string, object>
            //    {
            //        { "DynamicFilter_Broker_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
            //    });
            //DropTable("dbo.tblInsurance",
            //    removedAnnotations: new Dictionary<string, object>
            //    {
            //        { "DynamicFilter_Insurance_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
            //    });
            //DropTable("dbo.tblManufacture",
            //    removedAnnotations: new Dictionary<string, object>
            //    {
            //        { "DynamicFilter_Manufacture_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
            //    });
            //DropTable("dbo.tblVehicleModel",
            //    removedAnnotations: new Dictionary<string, object>
            //    {
            //        { "DynamicFilter_VehicleModel_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
            //    });
            //DropTable("dbo.JobDetails_StoredProc",
            //    removedAnnotations: new Dictionary<string, object>
            //    {
            //        { "DynamicFilter_JobDetails_StoredProc_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
            //    });
            //DropTable("dbo.PbPersons",
            //    removedAnnotations: new Dictionary<string, object>
            //    {
            //        { "DynamicFilter_Person_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
            //    });
            //DropTable("dbo.tblVendors",
            //    removedAnnotations: new Dictionary<string, object>
            //    {
            //        { "DynamicFilter_Vendor_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
            //    });
            DropStoredProcedure("dbo.JobDetails_StoredProc_Insert");
            DropStoredProcedure("dbo.JobDetails_StoredProc_Update");
            DropStoredProcedure("dbo.JobDetails_StoredProc_Delete");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tblVendors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        SupplierCode = c.Guid(nullable: false),
                        SupplierName = c.String(),
                        ContactName = c.String(),
                        ContactPhone = c.String(),
                        ContactFax = c.String(),
                        ContactEmail = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        Location = c.String(),
                        RegistrationNumber = c.String(),
                        TaxRegistrationNumber = c.String(),
                        SupplierAccount = c.String(),
                        PaymentTerms = c.String(),
                        AccountNumber = c.String(),
                        Type = c.String(),
                        Branch = c.String(),
                        CurrencyID = c.Int(nullable: false),
                        BankID = c.Int(nullable: false),
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
                    { "DynamicFilter_Vendor_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PbPersons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 32),
                        Surname = c.String(nullable: false, maxLength: 32),
                        EmailAddress = c.String(maxLength: 255),
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
                    { "DynamicFilter_Person_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobDetails_StoredProc",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Title = c.String(),
                        Email = c.String(),
                        Tel = c.String(),
                        CommunicationType = c.String(),
                        ContactAfterService = c.String(),
                        RegNo = c.String(),
                        VinNumber = c.String(),
                        Colour = c.String(),
                        Year = c.String(),
                        UnderWaranty = c.String(),
                        New_Comeback = c.String(),
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
                    { "DynamicFilter_JobDetails_StoredProc_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblVehicleModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManufactureID = c.Int(nullable: false),
                        Model_Desc = c.String(nullable: false, maxLength: 500),
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
                    { "DynamicFilter_VehicleModel_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblManufacture",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Manufacture_Desc = c.String(nullable: false, maxLength: 500),
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
                    { "DynamicFilter_Manufacture_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblInsurance",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Insurance_Desc = c.String(nullable: false, maxLength: 500),
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
                    { "DynamicFilter_Insurance_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblBroker",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Broker_Desc = c.String(nullable: false, maxLength: 500),
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
                    { "DynamicFilter_Broker_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.tblVendors", "BankID");
            CreateIndex("dbo.tblVendors", "CurrencyID");
            CreateIndex("dbo.brJobs", "ModelID");
            AddForeignKey("dbo.tblVendors", "CurrencyID", "dbo.tblCurrency", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tblVendors", "BankID", "dbo.tblBanks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.brJobs", "ModelID", "dbo.tblVehicleModel", "Id", cascadeDelete: true);
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
