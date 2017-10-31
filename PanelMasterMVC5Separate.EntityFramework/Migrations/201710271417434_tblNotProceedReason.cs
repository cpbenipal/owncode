namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class tblNotProceedReason : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblNotProceedReason",
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
                    { "DynamicFilter_NotProceedReason_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.brJobs", "NotProceedID", c => c.Int(nullable: false));
            AddColumn("dbo.brJobs", "NotProceedReason_Id", c => c.Int());
            CreateIndex("dbo.brJobs", "NotProceedReason_Id");
            AddForeignKey("dbo.brJobs", "NotProceedReason_Id", "dbo.tblNotProceedReason", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.brJobs", "NotProceedReason_Id", "dbo.tblNotProceedReason");
            DropIndex("dbo.brJobs", new[] { "NotProceedReason_Id" });
            DropColumn("dbo.brJobs", "NotProceedReason_Id");
            DropColumn("dbo.brJobs", "NotProceedID");
            DropTable("dbo.tblNotProceedReason",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_NotProceedReason_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
