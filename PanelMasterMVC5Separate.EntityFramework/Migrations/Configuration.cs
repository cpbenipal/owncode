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
            AddTenantSignonPlans(context);
            context.SaveChanges();
        }

        private void AddTenantSignonPlans(PanelMasterMVC5SeparateDbContext context)
        {
            var data = new List<SignonPlans>();
            data.AddRange(GetPlans());
            context.SignonPlan.AddOrUpdate(data.ToArray());
        }
        private static IEnumerable<SignonPlans> GetPlans()
        {
            yield return SignonPlans("Bugdet", 24.00, "blue" , 3);
            yield return SignonPlans("Solo", 39.00 , "red" , 5 );
            yield return SignonPlans("Start Up", 59.00, "green" , 20);
            yield return SignonPlans("Enterprise", 128.00, "purple", 100);
        }
        public static SignonPlans SignonPlans(string planname, double price, string color, int member)
        {
            return new SignonPlans()
            {
                PlanName = planname,
                Price = price,
                HeaderColor = color,
                Members = member
            };
        }
    }
}
