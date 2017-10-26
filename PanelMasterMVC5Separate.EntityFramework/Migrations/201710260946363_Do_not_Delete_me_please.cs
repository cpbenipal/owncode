namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Do_not_Delete_me_please : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpRoles", "RolesCategoryID", c => c.Int());
        }

        public override void Down()
        {
            AddColumn("dbo.AbpRoles", "RolesCategoryID", c => c.Int(nullable: false));
        }
    }
}
