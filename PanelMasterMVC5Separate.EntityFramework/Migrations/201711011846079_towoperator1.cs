namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class towoperator1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.brJobs", "TowOperatorID", "dbo.tblTowOperator");
            DropIndex("dbo.brJobs", new[] { "TowOperatorID" });
            RenameColumn(table: "dbo.brJobs", name: "TowOperatorID", newName: "TowOperator_Id");
            AlterColumn("dbo.brJobs", "TowOperator_Id", c => c.Int());
            CreateIndex("dbo.brJobs", "TowOperator_Id");
            AddForeignKey("dbo.brJobs", "TowOperator_Id", "dbo.tblTowOperator", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.brJobs", "TowOperator_Id", "dbo.tblTowOperator");
            DropIndex("dbo.brJobs", new[] { "TowOperator_Id" });
            AlterColumn("dbo.brJobs", "TowOperator_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.brJobs", name: "TowOperator_Id", newName: "TowOperatorID");
            CreateIndex("dbo.brJobs", "TowOperatorID");
            AddForeignKey("dbo.brJobs", "TowOperatorID", "dbo.tblTowOperator", "Id", cascadeDelete: true);
        }
    }
}
