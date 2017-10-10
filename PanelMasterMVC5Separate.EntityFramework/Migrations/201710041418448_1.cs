namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tblVendorSub", "RegistrationNumber");
            DropColumn("dbo.tblVendorSub", "TaxRegistrationNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblVendorSub", "TaxRegistrationNumber", c => c.String());
            AddColumn("dbo.tblVendorSub", "RegistrationNumber", c => c.String());
        }
    }
}
