using EntityFramework.DynamicFilters;
using PanelMasterMVC5Separate.EntityFramework;

namespace PanelMasterMVC5Separate.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly PanelMasterMVC5SeparateDbContext _context;
        private readonly int _tenantId;

        public TestDataBuilder(PanelMasterMVC5SeparateDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new TestOrganizationUnitsBuilder(_context, _tenantId).Create();

            _context.SaveChanges();
        }
    }
}
