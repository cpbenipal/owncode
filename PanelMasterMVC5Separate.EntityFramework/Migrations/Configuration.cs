using System;
using System.Data.Entity.Migrations;
using Abp.Events.Bus;
using Abp.Events.Bus.Entities;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using EntityFramework.DynamicFilters;
using PanelMasterMVC5Separate.EntityFramework;
using PanelMasterMVC5Separate.Migrations.Seed.Host;
using PanelMasterMVC5Separate.Migrations.Seed.Tenants;
using System.Collections.Generic;
using PanelMasterMVC5Separate.MultiTenancy;
using System.Data.Entity;

namespace PanelMasterMVC5Separate.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<EntityFramework.PanelMasterMVC5SeparateDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true; 
            ContextKey = "PanelMasterMVC5Separate";
        }

        protected override void Seed(EntityFramework.PanelMasterMVC5SeparateDbContext context)
        {
            context.DisableAllFilters();

            context.EntityChangeEventHelper = NullEntityChangeEventHelper.Instance;
            context.EventBus = NullEventBus.Instance;

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantBuilder(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                
            }
            
            context.SaveChanges();
        }         
    }

    public class PanelMasterDBInitializer : CreateDatabaseIfNotExists<EntityFramework.PanelMasterMVC5SeparateDbContext>
    {
        protected override void Seed(PanelMasterMVC5SeparateDbContext context)
        {
            //You can add seed for tenant databases using Tenant property...
            //new DefaultDataFirstTimeMigration(context).CreateTowOperators(); // Add default tow operators

            new DefaultDataFirstTimeMigration(context).CreateBanks(); // Add default CreateBanks

            new DefaultDataFirstTimeMigration(context).CreateBrokerMaster(); // Add default CreateBrokerMaster

            new DefaultDataFirstTimeMigration(context).CreateInsurerMaster(); // Add default CreateInsurerMaster

            new DefaultDataFirstTimeMigration(context).CreateTowOperator(); // Add default CreateTowOperator

            new DefaultDataFirstTimeMigration(context).CreateVehicleMakes(); // Add default CreateVehicleMakes

            new DefaultDataFirstTimeMigration(context).CreateVehicleModels(); // Add default CreateVehicleModels

            new DefaultDataFirstTimeMigration(context).CreateVendorMain(); // Add default CreateVendorMain

            base.Seed(context);
        }
    }
}
