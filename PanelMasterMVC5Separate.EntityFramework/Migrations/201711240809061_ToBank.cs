namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToBank : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblBanks", "isActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblBanks", "isActive");
        }
    }
}
