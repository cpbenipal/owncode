namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CalculatedVals : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblQuoteMaster", "TotalQuotedValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tblQuoteMaster", "EstimatedRepairDays", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tblQuoteMaster", "RepairerEstimatedDays", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblQuoteMaster", "RepairerEstimatedDays");
            DropColumn("dbo.tblQuoteMaster", "EstimatedRepairDays");
            DropColumn("dbo.tblQuoteMaster", "TotalQuotedValue");
        }
    }
}
