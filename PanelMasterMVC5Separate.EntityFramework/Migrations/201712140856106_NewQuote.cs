namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewQuote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblQuoteMaster", "IsStructuralRepairWork", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblQuoteMaster", "IsStructuralRepairWork");
        }
    }
}
