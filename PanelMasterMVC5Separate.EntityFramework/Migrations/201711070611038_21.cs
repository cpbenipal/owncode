namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class _21 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblTenantPlanBillingDetails", "planId", "dbo.tblSignonPlans");
            DropForeignKey("dbo.tblTenantPlanBillingDetails", "TenantId", "dbo.AbpTenants");
            DropForeignKey("dbo.tblTenantProfile", "TenantId", "dbo.AbpTenants");
            DropIndex("dbo.tblTenantPlanBillingDetails", new[] { "TenantId" });
            DropIndex("dbo.tblTenantPlanBillingDetails", new[] { "planId" });
            DropIndex("dbo.tblTenantProfile", new[] { "TenantId" });
            DropTable("dbo.tblTenantPlanBillingDetails",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TenantPlanBillingDetails_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.tblTenantProfile",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TenantProfile_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tblTenantProfile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 32),
                        CellNumber = c.String(nullable: false, maxLength: 24),
                        PhoneNumber = c.String(nullable: false, maxLength: 24),
                        CompanyName = c.String(nullable: false, maxLength: 32),
                        CompanyRegistrationNo = c.String(nullable: false, maxLength: 32),
                        CompanyVatNo = c.String(nullable: false, maxLength: 32),
                        Address = c.String(nullable: false),
                        City = c.String(nullable: false, maxLength: 32),
                        CountryCode = c.String(nullable: false, maxLength: 2),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tblTenantPlanBillingDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenantId = c.Int(nullable: false),
                        planId = c.Int(nullable: false),
                        BillingCountryCode = c.String(nullable: false, maxLength: 2),
                        CurrencyCode = c.String(nullable: false, maxLength: 3),
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.tblTenantProfile", "TenantId");
            CreateIndex("dbo.tblTenantPlanBillingDetails", "planId");
            CreateIndex("dbo.tblTenantPlanBillingDetails", "TenantId");
            AddForeignKey("dbo.tblTenantProfile", "TenantId", "dbo.AbpTenants", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tblTenantPlanBillingDetails", "TenantId", "dbo.AbpTenants", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tblTenantPlanBillingDetails", "planId", "dbo.tblSignonPlans", "Id", cascadeDelete: true);
        }
    }
}
