namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeToPreAuthCol : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblQuoteMaster", "Pre_Auth", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblQuoteMaster", "Pre_Auth", c => c.Int(nullable: false));
        }
    }
}
