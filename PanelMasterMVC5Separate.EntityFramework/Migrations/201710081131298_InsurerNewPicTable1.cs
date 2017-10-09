namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class InsurerNewPicTable1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblInsurerMasterPics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bytes = c.Binary(nullable: false),
                        InsurerID = c.Int(nullable: false),
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
                    { "DynamicFilter_InsurerPics_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblInsurerMaster", t => t.InsurerID, cascadeDelete: true)
                .Index(t => t.InsurerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblInsurerMasterPics", "InsurerID", "dbo.tblInsurerMaster");
            DropIndex("dbo.tblInsurerMasterPics", new[] { "InsurerID" });
            DropTable("dbo.tblInsurerMasterPics",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_InsurerPics_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
