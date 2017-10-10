namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblBanks", "BankName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblBanks", "BankName", c => c.String(nullable: false));
        }
    }
}
