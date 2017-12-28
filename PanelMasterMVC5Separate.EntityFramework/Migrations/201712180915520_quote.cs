namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class quote : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblQAction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Action = c.String(),
                        tblqparttypeId = c.Int(nullable: false),
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
                    { "DynamicFilter_QAction_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblQparttype", t => t.tblqparttypeId, cascadeDelete: true)
                .Index(t => t.tblqparttypeId);
            
            CreateTable(
                "dbo.tblQparttype",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Action = c.String(),
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
                    { "DynamicFilter_QPartType_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblQlocation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Location = c.String(),
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
                    { "DynamicFilter_QLocation_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblQuoteDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        tenantid = c.Int(),
                        QuoteId = c.Int(nullable: false),
                        quoteStatus = c.String(),
                        Actionid = c.Int(nullable: false),
                        Locationid = c.Int(nullable: false),
                        Description = c.String(),
                        ToOrder = c.Boolean(nullable: false),
                        Outwork = c.Boolean(nullable: false),
                        PartQty = c.Int(nullable: false),
                        PartPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Part = c.String(),
                        PanelHrs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PanelRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaintHrs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaintRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SAHrs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SARate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsCurrent = c.Boolean(nullable: false),
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
                    { "DynamicFilter_QuoteDetails_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblQAction", t => t.Actionid, cascadeDelete: true)
                .ForeignKey("dbo.tblQlocation", t => t.Locationid, cascadeDelete: true)
                .ForeignKey("dbo.tblQuoteMaster", t => t.QuoteId, cascadeDelete: true)
                .Index(t => t.QuoteId)
                .Index(t => t.Actionid)
                .Index(t => t.Locationid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblQuoteDetails", "QuoteId", "dbo.tblQuoteMaster");
            DropForeignKey("dbo.tblQuoteDetails", "Locationid", "dbo.tblQlocation");
            DropForeignKey("dbo.tblQuoteDetails", "Actionid", "dbo.tblQAction");
            DropForeignKey("dbo.tblQAction", "tblqparttypeId", "dbo.tblQparttype");
            DropIndex("dbo.tblQuoteDetails", new[] { "Locationid" });
            DropIndex("dbo.tblQuoteDetails", new[] { "Actionid" });
            DropIndex("dbo.tblQuoteDetails", new[] { "QuoteId" });
            DropIndex("dbo.tblQAction", new[] { "tblqparttypeId" });
            DropTable("dbo.tblQuoteDetails",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_QuoteDetails_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblQlocation",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_QLocation_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblQparttype",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_QPartType_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblQAction",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_QAction_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
