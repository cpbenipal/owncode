namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsurerPictureParamAddedNerwd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblInsurerMaster", "Bytes", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblInsurerMaster", "Bytes", c => c.Binary());
        }
    }
}
