namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class testmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblAttachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageId = c.Int(nullable: false),
                        Attachment = c.Binary(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Messages_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Attachments_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblMessages", t => t.Messages_Id)
                .Index(t => t.Messages_Id);
            
            CreateTable(
                "dbo.tblMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        TenantId = c.Int(),
                        Subject = c.String(),
                        Body = c.String(),
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
                    { "DynamicFilter_Messages_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblMessagesUserLinking",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageId = c.Int(nullable: false),
                        ToUserId = c.Long(nullable: false),
                        FromUserId = c.Long(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        IsTrashed = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        Messages_Id = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MessagesUserLinking_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblMessages", t => t.Messages_Id)
                .Index(t => t.Messages_Id);
            
            CreateTable(
                "dbo.uClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FN = c.String(),
                        LN = c.String(),
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
                    { "DynamicFilter_uClass_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
           // AddColumn("dbo.AbpUsers", "Occupation", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblMessagesUserLinking", "Messages_Id", "dbo.tblMessages");
            DropForeignKey("dbo.tblAttachments", "Messages_Id", "dbo.tblMessages");
            DropIndex("dbo.tblMessagesUserLinking", new[] { "Messages_Id" });
            DropIndex("dbo.tblAttachments", new[] { "Messages_Id" });
          //  DropColumn("dbo.AbpUsers", "Occupation");
            DropTable("dbo.uClasses",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_uClass_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblMessagesUserLinking",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_MessagesUserLinking_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblMessages",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Messages_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblAttachments",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Attachments_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
