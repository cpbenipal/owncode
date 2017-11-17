namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newtenant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblTenantProfile", "FaximileeNumber", c => c.String(maxLength: 24));
            AlterColumn("dbo.tblTenantProfile", "PhoneNumber", c => c.String(maxLength: 24));
            AlterColumn("dbo.tblTenantProfile", "CompanyName", c => c.String(maxLength: 32));
            AlterColumn("dbo.tblTenantProfile", "CompanyRegistrationNo", c => c.String(maxLength: 32));
            AlterColumn("dbo.tblTenantProfile", "CompanyVatNo", c => c.String(maxLength: 32));
            AlterColumn("dbo.tblTenantProfile", "Address", c => c.String());
            AlterColumn("dbo.tblTenantProfile", "City", c => c.String(maxLength: 32));
            DropColumn("dbo.tblTenantProfile", "Remarks");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblTenantProfile", "Remarks", c => c.String());
            AlterColumn("dbo.tblTenantProfile", "City", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.tblTenantProfile", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.tblTenantProfile", "CompanyVatNo", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.tblTenantProfile", "CompanyRegistrationNo", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.tblTenantProfile", "CompanyName", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.tblTenantProfile", "PhoneNumber", c => c.String(nullable: false, maxLength: 24));
            DropColumn("dbo.tblTenantProfile", "FaximileeNumber");
        }
    }
}
