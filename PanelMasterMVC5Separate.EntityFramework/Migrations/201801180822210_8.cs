namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.brJobs", "ShopAllocation", c => c.Int(nullable: false));
            AddColumn("dbo.brJobs", "HighPriority", c => c.Boolean(nullable: false));
            AddColumn("dbo.brJobs", "Contents", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.brJobs", "Contents");
            DropColumn("dbo.brJobs", "HighPriority");
            DropColumn("dbo.brJobs", "ShopAllocation");
        }
    }
}
