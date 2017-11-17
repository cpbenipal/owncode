namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class companylogo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblTenantCompanyLogo",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CompanyId = c.Int(),
                        Bytes = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblTenantCompanyLogo");
        }
    }
}
