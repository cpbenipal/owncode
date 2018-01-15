namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeQdetail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblQuoteDetails", "Actionid", "dbo.tblQAction");
            DropForeignKey("dbo.tblQuoteDetails", "Locationid", "dbo.tblQlocation");
            DropIndex("dbo.tblQuoteDetails", new[] { "Actionid" });
            DropIndex("dbo.tblQuoteDetails", new[] { "Locationid" });
            AddColumn("dbo.tblQuoteDetails", "QAction", c => c.String());
            AddColumn("dbo.tblQuoteDetails", "QLocation", c => c.String());
            DropColumn("dbo.tblQuoteDetails", "Actionid");
            DropColumn("dbo.tblQuoteDetails", "Locationid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblQuoteDetails", "Locationid", c => c.Int(nullable: false));
            AddColumn("dbo.tblQuoteDetails", "Actionid", c => c.Int(nullable: false));
            DropColumn("dbo.tblQuoteDetails", "QLocation");
            DropColumn("dbo.tblQuoteDetails", "QAction");
            CreateIndex("dbo.tblQuoteDetails", "Locationid");
            CreateIndex("dbo.tblQuoteDetails", "Actionid");
            AddForeignKey("dbo.tblQuoteDetails", "Locationid", "dbo.tblQlocation", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tblQuoteDetails", "Actionid", "dbo.tblQAction", "Id", cascadeDelete: true);
        }
    }
}
