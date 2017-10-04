using System.Data.Entity;
using System.Reflection;
using Abp.Events.Bus;
using Abp.Modules;
using Castle.MicroKernel.Registration;
using PanelMasterMVC5Separate.EntityFramework;

namespace PanelMasterMVC5Separate.Migrator
{
    [DependsOn(typeof(PanelMasterMVC5SeparateDataModule))]
    public class PanelMasterMVC5SeparateMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<PanelMasterMVC5SeparateDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(typeof(IEventBus), () =>
            {
                IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                );
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}