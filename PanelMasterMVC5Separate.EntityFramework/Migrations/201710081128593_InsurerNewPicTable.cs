namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsurerNewPicTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblInsurerMaster", "LogoPicture", c => c.String(nullable: false));
            DropColumn("dbo.tblInsurerMaster", "Logo");
            DropColumn("dbo.tblInsurerMaster", "Bytes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblInsurerMaster", "Bytes", c => c.Binary(nullable: false));
            AddColumn("dbo.tblInsurerMaster", "Logo", c => c.String(nullable: false));
            DropColumn("dbo.tblInsurerMaster", "LogoPicture");
        }
    }
}
