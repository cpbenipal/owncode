namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addoccupationfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpUsers", "Occupation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpUsers", "Occupation");
        }
    }
}
