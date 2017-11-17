namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTenantStructnew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblTenantProfile", "CurrencyCode", c => c.String(maxLength: 3));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblTenantProfile", "CurrencyCode");
        }
    }
}
