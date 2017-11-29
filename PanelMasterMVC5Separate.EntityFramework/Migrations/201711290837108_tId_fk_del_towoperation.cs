namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tId_fk_del_towoperation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblTowOperator", "TenantId", "dbo.AbpTenants");
            DropIndex("dbo.tblTowOperator", new[] { "TenantId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.tblTowOperator", "TenantId");
            AddForeignKey("dbo.tblTowOperator", "TenantId", "dbo.AbpTenants", "Id", cascadeDelete: true);
        }
    }
}
