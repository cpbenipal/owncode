namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnotaxvalparameter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblQuoteDetails", "NoTaxVat", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblQuoteDetails", "NoTaxVat");
        }
    }
}
