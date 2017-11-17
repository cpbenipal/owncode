namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTenantStruct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblTenantProfile", "CountryCode", c => c.String(maxLength: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblTenantProfile", "CountryCode", c => c.String(nullable: false, maxLength: 2));
        }
    }
}
