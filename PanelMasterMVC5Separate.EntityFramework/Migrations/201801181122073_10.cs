namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.brJobs", "CurrentKMs", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.brJobs", "CurrentKMs", c => c.String());
        }
    }
}
