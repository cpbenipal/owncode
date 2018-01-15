namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.brJobs", "New_Comeback", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.brJobs", "New_Comeback", c => c.Boolean(nullable: false));
        }
    }
}
