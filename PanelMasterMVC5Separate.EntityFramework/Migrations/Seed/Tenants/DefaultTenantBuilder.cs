using System.Linq;
using PanelMasterMVC5Separate.Editions;
using PanelMasterMVC5Separate.EntityFramework;
using System.Collections.Generic;
using PanelMasterMVC5Separate.Vehicle;
using System.Data.Entity.Migrations;

namespace PanelMasterMVC5Separate.Migrations.Seed.Tenants
{
    public class DefaultTenantBuilder
    {
        private readonly PanelMasterMVC5SeparateDbContext _context;

        public DefaultTenantBuilder(PanelMasterMVC5SeparateDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateDefaultTenant();
            CreateDefaultPaintTypes();
        }

        private void CreateDefaultTenant()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == MultiTenancy.Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                defaultTenant = new MultiTenancy.Tenant(MultiTenancy.Tenant.DefaultTenantName, MultiTenancy.Tenant.DefaultTenantName);

                var defaultEdition = _context.Editions.FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
                if (defaultEdition != null)
                {
                    defaultTenant.EditionId = defaultEdition.Id;
                }

                _context.Tenants.Add(defaultTenant);
                _context.SaveChanges();
            }
        }
        

        private void CreateDefaultPaintTypes()
        {
            var data = new List<PaintTypes>();
            data.AddRange(GetDefaultPaintTypes());
            _context.PaintTypes.AddOrUpdate(data.ToArray());
            _context.SaveChanges();
        }
        private IEnumerable<PaintTypes> GetDefaultPaintTypes()
        {
            yield return PaintTypes("Solid");
            yield return PaintTypes("Metallic");
            yield return PaintTypes("Pearlescent");
            yield return PaintTypes("Matte");
        }

        private PaintTypes PaintTypes(string name)
        {
            if (!_context.PaintTypes.Any(x => x.PaintType == name))
            {
                return new PaintTypes()
                {
                    PaintType = name
                };
            }
            else
                return new PaintTypes() { };
        }
    }
}
