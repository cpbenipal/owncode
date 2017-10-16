namespace PanelMasterMVC5Separate.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class removestoredprocanditsmappings : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.JobDetails_StoredProc",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobDetails_StoredProc_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropStoredProcedure("dbo.JobDetails_StoredProc_Insert");
            DropStoredProcedure("dbo.JobDetails_StoredProc_Update");
            DropStoredProcedure("dbo.JobDetails_StoredProc_Delete");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.JobDetails_StoredProc",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Title = c.String(),
                        Email = c.String(),
                        Tel = c.String(),
                        CommunicationType = c.String(),
                        ContactAfterService = c.String(),
                        RegNo = c.String(),
                        VinNumber = c.String(),
                        Colour = c.String(),
                        Year = c.String(),
                        UnderWaranty = c.String(),
                        New_Comeback = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobDetails_StoredProc_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
