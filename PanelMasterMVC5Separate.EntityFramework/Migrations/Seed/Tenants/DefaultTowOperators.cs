using PanelMasterMVC5Separate.Claim;
using PanelMasterMVC5Separate.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Migrations.Seed.Tenants
{
    public class DefaultTowOperators
    {
        private readonly PanelMasterMVC5SeparateDbContext _context;
        public DefaultTowOperators(PanelMasterMVC5SeparateDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateDefaultTowOperators();
        }

        private void CreateDefaultTowOperators()
        {
            var defaultTenant = _context.Tenants.Select(x => x.Id).ToList();
            if (defaultTenant != null)
            {
                var data = new List<TowOperator>();
                foreach (var s in defaultTenant)
                {
                    data.AddRange(GetTowOperators(s));
                }
                _context.TowOperators.AddOrUpdate(data.ToArray());
                _context.SaveChanges();
            }
        }
        private IEnumerable<TowOperator> GetTowOperators(int tenantId)
        {
            string allstatus = "1 TIME TOWING,112 AUTOROADSIDE,A1 ASSIST,AA TOWING,ABOVE TOWING,ABS TOWING,ABSOLUTE TOWING,ACJ TOWING," +
               "ADNANCED RECOVERIES,AFRICA TOWING,AGT TOWING,ALBERTON TOWING,ALL WAYS TOWING,ALLTOW SERVICES,AM TOWING,ATS TOWING,AUTO ACCIDENT ASSIST," +
               "AUTO TECH TOWING,AUTOHAUS TOWING,BAPELA TOWING,BEUKES TOWING,BIG D ROADSIDE ASSIST,CAS TOWING,CENTOW TOWING,CLASSIQUE TOWING,DA TOWING," +
               "DAANTJIES TOWING,DC TOWING,DIVERSE TOWING &LOGISTICS,DOT TOWING,EAGLE TOWING,EASYWAY TOWING,EXCLUSIVE TOWING,EXECUTIVE CARRIERS,EXTREME TOWING," +
               "FIRST ROAD EMERGENCY,FLEETSIDE TOWING,FREDS AUTOBODY,GLOBAL TOW ASSIST,GLYNMART TOWING,J.J TOWING,JIDZ RECOVERIES,JML TOWING,MAGALIES AUTO CENTRE," +
               "MAGIC TOWING,METRO ACCIDENT ASSISTANCE,MILLENIUM TOWING,MIRACLE TOWING,MOMOS TOWING,NEWLANDS TOWING,NONE,ON CALL TOWING,OTHER,PJ'S TOWING,SEDS 24 HOUR TOWING," +
               "SNAP 123 TOWING,SOUTHSIDE TOWING,UNIQUE TOWING";

            string[] str = allstatus.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                if (!_context.TowOperators.Any(x => x.TenantId == tenantId && x.Description == str[i]))
                    yield return GetTowOperator(str[i], tenantId);
            }
        }
        private TowOperator GetTowOperator(string desc, int tenantId)
        {
            return new TowOperator()
            {
                Description = desc,
                TenantId = tenantId,
                isActive = false
            };
        }
    }
}
