namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class banktbl : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tblBanks", "Country_Id", "dbo.tblCoutries");
            DropIndex("dbo.tblBanks", new[] { "Country_Id" });
            AddColumn("dbo.tblBanks", "CountryCode", c => c.String());
            DropColumn("dbo.tblBanks", "Country_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblBanks", "Country_Id", c => c.Int());
            DropColumn("dbo.tblBanks", "CountryCode");
            CreateIndex("dbo.tblBanks", "Country_Id");
            AddForeignKey("dbo.tblBanks", "Country_Id", "dbo.tblCoutries", "Id");
        }
    }
}
