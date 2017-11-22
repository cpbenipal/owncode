namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Milestone3change : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.tblTenantProfile", "CountryCode", c => c.String(nullable: false, maxLength: 2));
            //AlterColumn("dbo.tblTenantProfile", "CurrencyCode", c => c.String(nullable: false, maxLength: 3));
            //DropColumn("dbo.tblTenantPlanBillingDetails", "BillingCountryCode");
            //DropColumn("dbo.tblTenantPlanBillingDetails", "CurrencyCode");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.tblTenantPlanBillingDetails", "CurrencyCode", c => c.String(nullable: false, maxLength: 3));
            //AddColumn("dbo.tblTenantPlanBillingDetails", "BillingCountryCode", c => c.String(nullable: false, maxLength: 2));
            //AlterColumn("dbo.tblTenantProfile", "CurrencyCode", c => c.String(maxLength: 3));
            //AlterColumn("dbo.tblTenantProfile", "CountryCode", c => c.String(maxLength: 2));
        }
    }
}
