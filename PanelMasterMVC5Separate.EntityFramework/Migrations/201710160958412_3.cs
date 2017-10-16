namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblVendors", "BankID", "dbo.tblBanks");
            DropForeignKey("dbo.tblVendors", "CurrencyID", "dbo.tblCurrency");
            DropIndex("dbo.tblVendors", new[] { "CurrencyID" });
            DropIndex("dbo.tblVendors", new[] { "BankID" });
            DropTable("dbo.PbPersons",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Person_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblVendors",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Vendor_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
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
            
            CreateIndex("dbo.tblVendors", "BankID");
            CreateIndex("dbo.tblVendors", "CurrencyID");
            AddForeignKey("dbo.tblVendors", "CurrencyID", "dbo.tblCurrency", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tblVendors", "BankID", "dbo.tblBanks", "Id", cascadeDelete: true);
        }
    }
}
