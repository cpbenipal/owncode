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
                "dbo.tblBrokerMasterPics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bytes = c.Binary(nullable: false),
                        BrokerID = c.Int(nullable: false),
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
                    { "DynamicFilter_BrokerMasterPics_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblBrokerMaster", t => t.BrokerID, cascadeDelete: true)
                .Index(t => t.BrokerID);
            
            CreateTable(
                "dbo.tblBrokerMaster",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrokerName = c.String(nullable: false),
                        LogoPicture = c.String(nullable: false),
                        Mask = c.String(nullable: false),
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
                    { "DynamicFilter_BrokerMaster_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblBrokerSubMaster",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantID = c.Int(nullable: false),
                        BrokerID = c.Int(nullable: false),
                        SpeedbumpEmail = c.String(nullable: false),
                        QuoteCentreEmail = c.String(nullable: false),
                        Mask = c.String(nullable: false),
                        EarlySettleDisc = c.String(nullable: false),
                        ContactName = c.String(nullable: false),
                        ContactPhone = c.String(nullable: false),
                        ContactFax = c.String(nullable: false),
                        ContactEmail = c.String(nullable: false),
                        Address1 = c.String(nullable: false),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        Location = c.String(nullable: false),
                        RegistrationNumber = c.String(nullable: false),
                        TaxRegistrationNumber = c.String(nullable: false),
                        BrokerAccount = c.String(nullable: false),
                        PaymentTerms = c.String(nullable: false),
                        AccountNumber = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        Branch = c.String(nullable: false),
                        Comments = c.String(),
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
                    { "DynamicFilter_BrokerSubMaster_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblBanks", t => t.BankID, cascadeDelete: true)
                .ForeignKey("dbo.tblBrokerMaster", t => t.BrokerID, cascadeDelete: true)
                .ForeignKey("dbo.tblCurrency", t => t.CurrencyID, cascadeDelete: true)
                .Index(t => t.BrokerID)
                .Index(t => t.CurrencyID)
                .Index(t => t.BankID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblBrokerSubMaster", "CurrencyID", "dbo.tblCurrency");
            DropForeignKey("dbo.tblBrokerSubMaster", "BrokerID", "dbo.tblBrokerMaster");
            DropForeignKey("dbo.tblBrokerSubMaster", "BankID", "dbo.tblBanks");
            DropForeignKey("dbo.tblBrokerMasterPics", "BrokerID", "dbo.tblBrokerMaster");
            DropIndex("dbo.tblBrokerSubMaster", new[] { "BankID" });
            DropIndex("dbo.tblBrokerSubMaster", new[] { "CurrencyID" });
            DropIndex("dbo.tblBrokerSubMaster", new[] { "BrokerID" });
            DropIndex("dbo.tblBrokerMasterPics", new[] { "BrokerID" });
            DropTable("dbo.tblBrokerSubMaster",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BrokerSubMaster_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblBrokerMaster",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BrokerMaster_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblBrokerMasterPics",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BrokerMasterPics_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
