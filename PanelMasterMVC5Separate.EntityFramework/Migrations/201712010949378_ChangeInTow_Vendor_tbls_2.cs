namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeInTow_Vendor_tbls_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblTowOperatorSub", "TenantId", "dbo.AbpTenants");
            DropIndex("dbo.tblTowOperatorSub", new[] { "TenantId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.tblTowOperatorSub", "TenantId");
            AddForeignKey("dbo.tblTowOperatorSub", "TenantId", "dbo.AbpTenants", "Id", cascadeDelete: true);
        }
    }
}
