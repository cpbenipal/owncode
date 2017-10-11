using System.Data.Entity.Migrations;
using Abp.Events.Bus;
using Abp.Events.Bus.Entities;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using EntityFramework.DynamicFilters;
using PanelMasterMVC5Separate.Migrations.Seed.Host;
using PanelMasterMVC5Separate.Migrations.Seed.Tenants;

namespace PanelMasterMVC5Separate.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<EntityFramework.PanelMasterMVC5SeparateDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
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
                //You can add seed for tenant databases using Tenant property...
            }

            context.SaveChanges();
        }
    }
}
