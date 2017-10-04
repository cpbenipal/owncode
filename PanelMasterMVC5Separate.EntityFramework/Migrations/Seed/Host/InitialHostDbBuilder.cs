using PanelMasterMVC5Separate.EntityFramework;

namespace PanelMasterMVC5Separate.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly PanelMasterMVC5SeparateDbContext _context;

        public InitialHostDbBuilder(PanelMasterMVC5SeparateDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
