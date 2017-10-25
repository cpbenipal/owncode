namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class ADDQUOTETABLESTODB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblQuoteCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Enabled = c.Boolean(nullable: false),
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
                    { "DynamicFilter_QuoteCategories_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblQuoteMaster",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(nullable: false),
                        JobId = c.Int(nullable: false),
                        QuoteStatusID = c.Int(nullable: false),
                        QuoteCatID = c.Int(nullable: false),
                        RepairTypeId = c.Int(nullable: false),
                        Pre_Auth = c.Int(nullable: false),
                        Value = c.String(nullable: false),
                        Comments = c.String(),
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
                    { "DynamicFilter_QuoteMaster_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.brJobs", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.tblQuoteCategories", t => t.QuoteCatID, cascadeDelete: true)
                .ForeignKey("dbo.tblQuoteStatus", t => t.QuoteStatusID, cascadeDelete: true)
                .ForeignKey("dbo.tblRepairType", t => t.RepairTypeId, cascadeDelete: true)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId, cascadeDelete: true)
                .Index(t => t.TenantId)
                .Index(t => t.JobId)
                .Index(t => t.QuoteStatusID)
                .Index(t => t.QuoteCatID)
                .Index(t => t.RepairTypeId);
            
            CreateTable(
                "dbo.tblQuoteStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Enabled = c.Boolean(nullable: false),
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
                    { "DynamicFilter_QuoteStatus_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblRepairType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Enabled = c.Boolean(nullable: false),
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
                    { "DynamicFilter_RepairTypes_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblQuoteMaster", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.tblQuoteMaster", "RepairTypeId", "dbo.tblRepairType");
            DropForeignKey("dbo.tblQuoteMaster", "QuoteStatusID", "dbo.tblQuoteStatus");
            DropForeignKey("dbo.tblQuoteMaster", "QuoteCatID", "dbo.tblQuoteCategories");
            DropForeignKey("dbo.tblQuoteMaster", "JobId", "dbo.brJobs");
            DropIndex("dbo.tblQuoteMaster", new[] { "RepairTypeId" });
            DropIndex("dbo.tblQuoteMaster", new[] { "QuoteCatID" });
            DropIndex("dbo.tblQuoteMaster", new[] { "QuoteStatusID" });
            DropIndex("dbo.tblQuoteMaster", new[] { "JobId" });
            DropIndex("dbo.tblQuoteMaster", new[] { "TenantId" });
            DropTable("dbo.tblRepairType",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RepairTypes_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblQuoteStatus",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_QuoteStatus_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblQuoteMaster",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_QuoteMaster_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblQuoteCategories",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_QuoteCategories_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
