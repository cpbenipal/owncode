namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddToBrokerColChg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblBrokerMaster", "BrokerName", c => c.String(nullable: false));
            DropColumn("dbo.tblBrokerMaster", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblBrokerMaster", "Description", c => c.String(nullable: false));
            DropColumn("dbo.tblBrokerMaster", "BrokerName");
        }
    }
}
