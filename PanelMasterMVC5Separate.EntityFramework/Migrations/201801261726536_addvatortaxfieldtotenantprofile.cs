namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addvatortaxfieldtotenantprofile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblTenantProfile", "VatorTax", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblTenantProfile", "VatorTax");
        }
    }
}
