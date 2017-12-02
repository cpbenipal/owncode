namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeInTow_Vendor_tbls_24 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblTowOperatorSub", "isActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblTowOperatorSub", "isActive");
        }
    }
}
