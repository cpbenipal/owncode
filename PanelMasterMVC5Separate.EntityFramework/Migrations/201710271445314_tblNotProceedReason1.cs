namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblNotProceedReason1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.brJobs", "NotProceedID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.brJobs", "NotProceedID", c => c.Int(nullable: false));
        }
    }
}
