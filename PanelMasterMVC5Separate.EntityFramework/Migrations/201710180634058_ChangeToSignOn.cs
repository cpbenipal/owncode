namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeToSignOn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblSignonPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlanName = c.String(),
                        Price = c.Double(nullable: false),
                        HeaderColor = c.String(),
                        Members = c.Int(nullable: false),
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
                    { "DynamicFilter_SignonPlans_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblTenantPlanBillingDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(nullable: false),
                        planId = c.Int(nullable: false),
                        CardHoldersName = c.String(nullable: false, maxLength: 160),
                        CardNumber = c.String(nullable: false, maxLength: 16),
                        CardExpiration = c.String(nullable: false, maxLength: 7),
                        CVV = c.String(nullable: false, maxLength: 4),
                        PaymentOptions = c.String(),
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
                    { "DynamicFilter_TenantPlanBillingDetails_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblSignonPlans", t => t.planId, cascadeDelete: true)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId, cascadeDelete: true)
                .Index(t => t.TenantId)
                .Index(t => t.planId);
            
            CreateTable(
                "dbo.tblTenantProfile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 32),
                        PhoneNumber = c.String(nullable: false, maxLength: 24),
                        CompanyName = c.String(nullable: false, maxLength: 32),
                        CompanyRegistrationNo = c.String(nullable: false, maxLength: 32),
                        CompanyVatNo = c.String(nullable: false, maxLength: 32),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false, maxLength: 32),
                        Country_list = c.String(nullable: false),
                        Remarks = c.String(),
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
                    { "DynamicFilter_TenantProfile_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpTenants", t => t.TenantId, cascadeDelete: true)
                .Index(t => t.TenantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblTenantProfile", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.tblTenantPlanBillingDetails", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.tblTenantPlanBillingDetails", "planId", "dbo.tblSignonPlans");
            DropIndex("dbo.tblTenantProfile", new[] { "TenantId" });
            DropIndex("dbo.tblTenantPlanBillingDetails", new[] { "planId" });
            DropIndex("dbo.tblTenantPlanBillingDetails", new[] { "TenantId" });
            DropTable("dbo.tblTenantProfile",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TenantProfile_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblTenantPlanBillingDetails",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TenantPlanBillingDetails_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblSignonPlans",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SignonPlans_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
