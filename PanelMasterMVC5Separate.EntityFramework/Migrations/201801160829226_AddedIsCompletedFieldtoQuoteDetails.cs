namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsCompletedFieldtoQuoteDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblQuoteDetails", "IsCompleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblQuoteDetails", "IsCompleted");
        }
    }
}
