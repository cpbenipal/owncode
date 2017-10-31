namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobStatusesTables1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblJobstatusTenant", "isActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.tblJobstatusTenant", "Enabled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblJobstatusTenant", "Enabled", c => c.Boolean(nullable: false));
            DropColumn("dbo.tblJobstatusTenant", "isActive");
        }
    }
}
