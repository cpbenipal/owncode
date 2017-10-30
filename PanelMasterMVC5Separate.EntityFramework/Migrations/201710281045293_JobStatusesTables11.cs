namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobStatusesTables11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblJobstatusTenant", "ShowSpeedbump", c => c.Boolean(nullable: false));
            AlterColumn("dbo.tblJobstatusTenant", "ShowAwaiting", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblJobstatusTenant", "ShowAwaiting", c => c.Int(nullable: false));
            AlterColumn("dbo.tblJobstatusTenant", "ShowSpeedbump", c => c.Int(nullable: false));
        }
    }
}
