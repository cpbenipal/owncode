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
                "dbo.tblInsurerMaster",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    InsurerName = c.String(nullable: false),
                    Logo = c.String(nullable: false),
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
                    { "DynamicFilter_InsurerMaster_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.tblInsurerSubMaster",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TenantID = c.Int(nullable: false),
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
                    InsurerAccount = c.String(nullable: false),
                    PaymentTerms = c.String(nullable: false),
                    AccountNumber = c.String(nullable: false),
                    Type = c.String(nullable: false),
                    Branch = c.String(nullable: false),
                    Comments = c.String(),
                    InsurerID = c.Int(nullable: false),
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
                    { "DynamicFilter_InsurerSub_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblBanks", t => t.BankID, cascadeDelete: true)
                .ForeignKey("dbo.tblCurrency", t => t.CurrencyID, cascadeDelete: true)
                .ForeignKey("dbo.tblInsurerMaster", t => t.InsurerID, cascadeDelete: true)
                .Index(t => t.InsurerID)
                .Index(t => t.CurrencyID)
                .Index(t => t.BankID);


        }

        public override void Down()
        {
            DropForeignKey("dbo.tblInsurerSubMaster", "InsurerID", "dbo.tblInsurerMaster");
            DropForeignKey("dbo.tblInsurerSubMaster", "CurrencyID", "dbo.tblCurrency");
            DropForeignKey("dbo.tblInsurerSubMaster", "BankID", "dbo.tblBanks");

            DropIndex("dbo.tblInsurerSubMaster", new[] { "BankID" });
            DropIndex("dbo.tblInsurerSubMaster", new[] { "CurrencyID" });
            DropIndex("dbo.tblInsurerSubMaster", new[] { "InsurerID" });

            DropTable("dbo.tblInsurerSubMaster",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_InsurerSub_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblInsurerMaster",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_InsurerMaster_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });

        }
    }
}
