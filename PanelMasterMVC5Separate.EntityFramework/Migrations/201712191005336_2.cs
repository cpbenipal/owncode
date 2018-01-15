namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.brJobs", "JobStatusID");
            AddForeignKey("dbo.brJobs", "JobStatusID", "dbo.tblJobstatus", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.brJobs", "JobStatusID", "dbo.tblJobstatus");
            DropIndex("dbo.brJobs", new[] { "JobStatusID" });
        }
    }
}
