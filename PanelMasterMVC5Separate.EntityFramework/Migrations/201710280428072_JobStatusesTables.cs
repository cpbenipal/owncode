namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class JobStatusesTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblJobstatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
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
                    { "DynamicFilter_Jobstatus_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblJobstatusMask",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description1 = c.String(),
                        Description2 = c.String(),
                        Description3 = c.String(),
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
                    { "DynamicFilter_JobstatusMask_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblJobstatusTenant",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobStatusID = c.Int(nullable: false),
                        Tenant = c.Int(nullable: false),
                        Enabled = c.Boolean(nullable: false),
                        ShowSpeedbump = c.Int(nullable: false),
                        ShowAwaiting = c.Int(nullable: false),
                        Sortorder = c.Int(nullable: false),
                        Mask = c.Int(nullable: false),
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
                    { "DynamicFilter_JobstatusTenant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblJobstatus", t => t.JobStatusID, cascadeDelete: true)
                .ForeignKey("dbo.tblJobstatusMask", t => t.Mask, cascadeDelete: true)
                .Index(t => t.JobStatusID)
                .Index(t => t.Mask);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblJobstatusTenant", "Mask", "dbo.tblJobstatusMask");
            DropForeignKey("dbo.tblJobstatusTenant", "JobStatusID", "dbo.tblJobstatus");
            DropIndex("dbo.tblJobstatusTenant", new[] { "Mask" });
            DropIndex("dbo.tblJobstatusTenant", new[] { "JobStatusID" });
            DropTable("dbo.tblJobstatusTenant",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobstatusTenant_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblJobstatusMask",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobstatusMask_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblJobstatus",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Jobstatus_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
