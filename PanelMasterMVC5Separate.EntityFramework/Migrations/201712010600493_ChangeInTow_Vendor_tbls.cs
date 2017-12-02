namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeInTow_Vendor_tbls : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tblTowOperator", newName: "TowOperators");
            AddColumn("dbo.tblVendorSub", "RegistrationNumber", c => c.String());
            AddColumn("dbo.tblVendorSub", "TaxRegistrationNumber", c => c.String());
            AlterColumn("dbo.TowOperators", "Description", c => c.String(nullable: false));
            DropColumn("dbo.TowOperators", "TenantId");
            DropColumn("dbo.TowOperators", "ContactNumber");
            DropColumn("dbo.TowOperators", "ContactPerson");
            DropColumn("dbo.TowOperators", "EmailAddress");
            DropColumn("dbo.tblVendorMain", "RegistrationNumber");
            DropColumn("dbo.tblVendorMain", "TaxRegistrationNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblVendorMain", "TaxRegistrationNumber", c => c.String());
            AddColumn("dbo.tblVendorMain", "RegistrationNumber", c => c.String());
            AddColumn("dbo.TowOperators", "EmailAddress", c => c.String());
            AddColumn("dbo.TowOperators", "ContactPerson", c => c.String());
            AddColumn("dbo.TowOperators", "ContactNumber", c => c.String());
            AddColumn("dbo.TowOperators", "TenantId", c => c.Int(nullable: false));
            AlterColumn("dbo.TowOperators", "Description", c => c.String());
            DropColumn("dbo.tblVendorSub", "TaxRegistrationNumber");
            DropColumn("dbo.tblVendorSub", "RegistrationNumber");
            RenameTable(name: "dbo.TowOperators", newName: "tblTowOperator");
        }
    }
}
