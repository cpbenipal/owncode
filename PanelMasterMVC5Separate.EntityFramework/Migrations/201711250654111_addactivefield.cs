namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addactivefield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblBrokerMaster", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.tblInsurerMaster", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.tblSignonPlans", "isActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblSignonPlans", "isActive");
            DropColumn("dbo.tblInsurerMaster", "IsActive");
            DropColumn("dbo.tblBrokerMaster", "IsActive");
        }
    }
}
