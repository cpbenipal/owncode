namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.brJobs", "ClaimAdministrator", c => c.String());
            AddColumn("dbo.brJobs", "ClaimNumber", c => c.String());
            AddColumn("dbo.brJobs", "InsuranceOtherInfo", c => c.String());
            AddColumn("dbo.brJobs", "PolicyNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.brJobs", "PolicyNumber");
            DropColumn("dbo.brJobs", "InsuranceOtherInfo");
            DropColumn("dbo.brJobs", "ClaimNumber");
            DropColumn("dbo.brJobs", "ClaimAdministrator");
        }
    }
}
