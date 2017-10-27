namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_rolesCategoryID_to_Roles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpRoles", "RoleCategoryID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpRoles", "RoleCategoryID");
        }
    }
}
