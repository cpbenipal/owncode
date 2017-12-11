namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class certainly : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.brINS", "ClaimNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.brINS", "ClaimNumber");
        }
    }
}
