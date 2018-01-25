namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmessagingstrui : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblAttachments", "Attachment", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblAttachments", "Attachment", c => c.Byte(nullable: false));
        }
    }
}
