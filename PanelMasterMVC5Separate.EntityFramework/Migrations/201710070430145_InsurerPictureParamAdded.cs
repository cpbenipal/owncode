namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsurerPictureParamAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblInsurerMaster", "Bytes", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblInsurerMaster", "Bytes");
        }
    }
}
