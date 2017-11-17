namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblTenantProfile", "Timezone", c => c.String());
            AddColumn("dbo.tblTenantProfile", "InvoicingInstruction", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblTenantProfile", "InvoicingInstruction");
            DropColumn("dbo.tblTenantProfile", "Timezone");
        }
    }
}
