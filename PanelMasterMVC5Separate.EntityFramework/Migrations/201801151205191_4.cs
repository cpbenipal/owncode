namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.brJobs", "NotProceedReason_Id", "dbo.tblNotProceedReason");
            DropForeignKey("dbo.brJobs", "TowOperator_Id", "dbo.tblTowOperator");
            DropIndex("dbo.brJobs", new[] { "NotProceedReason_Id" });
            DropIndex("dbo.brJobs", new[] { "TowOperator_Id" });
            AddColumn("dbo.brJobs", "ClaimHandlerID", c => c.Int(nullable: false));
            AddColumn("dbo.brJobs", "PartsBuyerID", c => c.Int(nullable: false));
            AddColumn("dbo.brJobs", "KeyAccountManagerID", c => c.Int(nullable: false));
            AddColumn("dbo.brJobs", "EstimatorID", c => c.Int(nullable: false));
            DropColumn("dbo.brJobs", "FinancialID");
            DropColumn("dbo.brJobs", "ProductiveStaffID");
            DropColumn("dbo.brJobs", "ClaimEventID");
            DropColumn("dbo.brJobs", "NotProceedReason_Id");
            DropColumn("dbo.brJobs", "TowOperator_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.brJobs", "TowOperator_Id", c => c.Int());
            AddColumn("dbo.brJobs", "NotProceedReason_Id", c => c.Int());
            AddColumn("dbo.brJobs", "ClaimEventID", c => c.Int(nullable: false));
            AddColumn("dbo.brJobs", "ProductiveStaffID", c => c.Int(nullable: false));
            AddColumn("dbo.brJobs", "FinancialID", c => c.Int(nullable: false));
            DropColumn("dbo.brJobs", "EstimatorID");
            DropColumn("dbo.brJobs", "KeyAccountManagerID");
            DropColumn("dbo.brJobs", "PartsBuyerID");
            DropColumn("dbo.brJobs", "ClaimHandlerID");
            CreateIndex("dbo.brJobs", "TowOperator_Id");
            CreateIndex("dbo.brJobs", "NotProceedReason_Id");
            AddForeignKey("dbo.brJobs", "TowOperator_Id", "dbo.tblTowOperator", "Id");
            AddForeignKey("dbo.brJobs", "NotProceedReason_Id", "dbo.tblNotProceedReason", "Id");
        }
    }
}
