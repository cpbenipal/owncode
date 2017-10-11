namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblVendorMain",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplierCode = c.Guid(nullable: false),
                        SupplierName = c.String(),
                        RegistrationNumber = c.String(),
                        TaxRegistrationNumber = c.String(),
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
                    { "DynamicFilter_VendorMain_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblVendorSub",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(),
                        VendorID = c.Int(nullable: false),
                        ContactName = c.String(),
                        ContactPhone = c.String(),
                        ContactFax = c.String(),
                        ContactEmail = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        Location = c.String(),
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
                        VendorMains_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VendorSub_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblBanks", t => t.BankID, cascadeDelete: true)
                .ForeignKey("dbo.tblCurrency", t => t.CurrencyID, cascadeDelete: true)
                .ForeignKey("dbo.tblVendorMain", t => t.VendorMains_Id)
                .Index(t => t.CurrencyID)
                .Index(t => t.BankID)
                .Index(t => t.VendorMains_Id);
            
            AlterColumn("dbo.tblBanks", "BankName", c => c.String());
            AlterColumn("dbo.tblCurrency", "CurrencyCode", c => c.String());
            AlterColumn("dbo.tblCurrency", "CurrencyType", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblVendorSub", "VendorMains_Id", "dbo.tblVendorMain");
            DropForeignKey("dbo.tblVendorSub", "CurrencyID", "dbo.tblCurrency");
            DropForeignKey("dbo.tblVendorSub", "BankID", "dbo.tblBanks");
            DropIndex("dbo.tblVendorSub", new[] { "VendorMains_Id" });
            DropIndex("dbo.tblVendorSub", new[] { "BankID" });
            DropIndex("dbo.tblVendorSub", new[] { "CurrencyID" });
            AlterColumn("dbo.tblCurrency", "CurrencyType", c => c.String(nullable: false));
            AlterColumn("dbo.tblCurrency", "CurrencyCode", c => c.String(nullable: false));
            AlterColumn("dbo.tblBanks", "BankName", c => c.String(nullable: false));
            DropTable("dbo.tblVendorSub",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VendorSub_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblVendorMain",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VendorMain_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
