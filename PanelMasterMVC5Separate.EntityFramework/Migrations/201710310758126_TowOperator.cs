namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class TowOperator : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblTowOperator",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(nullable: false),
                        Description = c.String(),
                        ContactNumber = c.String(),
                        ContactPerson = c.String(),
                        EmailAdress = c.String(),
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
                    { "DynamicFilter_TowOperator_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId, cascadeDelete: true)
                .Index(t => t.TenantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblTowOperator", "TenantId", "dbo.AbpTenants");
            DropIndex("dbo.tblTowOperator", new[] { "TenantId" });
            DropTable("dbo.tblTowOperator",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TowOperator_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
