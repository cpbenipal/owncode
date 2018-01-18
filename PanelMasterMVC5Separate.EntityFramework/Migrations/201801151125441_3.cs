namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.brJobs", "JobStatusID", c => c.Int(nullable: false));
            CreateIndex("dbo.brJobs", "JobStatusID");
            AddForeignKey("dbo.brJobs", "JobStatusID", "dbo.tblJobstatus", "Id", cascadeDelete: true);
            DropColumn("dbo.brJobs", "BranchID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.brJobs", "BranchID", c => c.Int(nullable: false));
            DropForeignKey("dbo.brJobs", "JobStatusID", "dbo.tblJobstatus");
            DropIndex("dbo.brJobs", new[] { "JobStatusID" });
            DropColumn("dbo.brJobs", "JobStatusID");
        }
    }
}
