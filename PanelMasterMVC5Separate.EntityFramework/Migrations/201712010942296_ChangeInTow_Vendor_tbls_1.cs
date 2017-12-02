namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeInTow_Vendor_tbls_1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TowOperators", newName: "tblTowOperator");
            CreateTable(
                "dbo.tblTowOperatorSub",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TowOperatorId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                        ContactNumber = c.String(),
                        ContactPerson = c.String(),
                        EmailAddress = c.String(),
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
                    { "DynamicFilter_TowSubOperator_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId, cascadeDelete: true)
                .ForeignKey("dbo.tblTowOperator", t => t.TowOperatorId, cascadeDelete: true)
                .Index(t => t.TowOperatorId)
                .Index(t => t.TenantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblTowOperatorSub", "TowOperatorId", "dbo.tblTowOperator");
            DropForeignKey("dbo.tblTowOperatorSub", "TenantId", "dbo.AbpTenants");
            DropIndex("dbo.tblTowOperatorSub", new[] { "TenantId" });
            DropIndex("dbo.tblTowOperatorSub", new[] { "TowOperatorId" });
            DropTable("dbo.tblTowOperatorSub",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TowSubOperator_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            RenameTable(name: "dbo.tblTowOperator", newName: "TowOperators");
        }
    }
}
