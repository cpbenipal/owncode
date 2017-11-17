using PanelMasterMVC5Separate.Brokers;
using PanelMasterMVC5Separate.Claim;
using PanelMasterMVC5Separate.EntityFramework;
using PanelMasterMVC5Separate.Insurer;
using PanelMasterMVC5Separate.Vehicle;
using PanelMasterMVC5Separate.Vendors;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelMasterMVC5Separate.Migrations.Seed.Tenants
{
    public class DefaultDataFirstTimeMigration
    {
        private readonly PanelMasterMVC5SeparateDbContext _context;
        public DefaultDataFirstTimeMigration(PanelMasterMVC5SeparateDbContext context)
        {
            _context = context;
        }

        public void CreateTowOperators()
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

        /// <summary>
        /// Default Banks
        /// </summary>
        public void CreateBanks()
        {
            CreateDefaultBanks();
        }
        private void CreateDefaultBanks()
        {
            var data = new List<Banks>();
            data.AddRange(GetDefaultBanks());
            _context.Banks.AddOrUpdate(data.ToArray());
            _context.SaveChanges();
        }
        private IEnumerable<Banks> GetDefaultBanks()
        {
            yield return Banks("OTHER");
            yield return Banks("NONE");
        }

        private Banks Banks(string name)
        {
            if (!_context.Banks.Any(x => x.BankName == name))
            {
                return new Banks()
                {
                    BankName = name
                };
            }
            else
                return new Banks() { };
        }

        /// <summary>
        /// CreateBrokerMaster
        /// </summary>
        public void CreateBrokerMaster()
        {
            CreateDefaultBrokerMaster();
        }
        private void CreateDefaultBrokerMaster()
        {
            var data = new List<BrokerMaster>();
            data.AddRange(GetDefaultBrokerMasters());
            _context.BrokerMasters.AddOrUpdate(data.ToArray());
            _context.SaveChanges();
        }
        private IEnumerable<BrokerMaster> GetDefaultBrokerMasters()
        {
            yield return BrokerMaster("OTHER","2132");
            yield return BrokerMaster("NONE","");
        }

        private BrokerMaster BrokerMaster(string name, string mask)
        {
            if (!_context.BrokerMasters.Any(x => x.BrokerName == name))
            {
                return new BrokerMaster()
                {
                    BrokerName = name,
                    LogoPicture = "NONE",
                    Mask = mask
                };
            }
            else
                return new BrokerMaster() { };
        }


        /// <summary>
        /// CreateInsurerMaster
        /// </summary>
        public void CreateInsurerMaster()
        {
            CreateDefaultInsurerMaster();
        }
        private void CreateDefaultInsurerMaster()
        {
            var data = new List<InsurerMaster>();
            data.AddRange(GetDefaultInsurerMasters());
            _context.InsurerMasters.AddOrUpdate(data.ToArray());
            _context.SaveChanges();
        }
        private IEnumerable<InsurerMaster> GetDefaultInsurerMasters()
        {
            yield return InsurerMaster("OTHER", "2132");
            yield return InsurerMaster("NONE", "");
        }

        private InsurerMaster InsurerMaster(string name, string mask)
        {
            if (!_context.InsurerMasters.Any(x => x.InsurerName == name))
            {
                return new InsurerMaster()
                {
                    InsurerName = name,
                    LogoPicture = "NONE",
                    Mask = mask
                };
            }
            else
                return new InsurerMaster() { };
        }

        /// <summary>
        /// CreateTowOperator
        /// </summary>
        public void CreateTowOperator()
        {
            CreateDefaultTowOperator();
        }
        private void CreateDefaultTowOperator()
        {
            var data = new List<TowOperator>();
            data.AddRange(GetDefaultTowOperators());
            _context.TowOperators.AddOrUpdate(data.ToArray());
            _context.SaveChanges();
        }
        private IEnumerable<TowOperator> GetDefaultTowOperators()
        {
            yield return TowOperator("OTHER");
            yield return TowOperator("NONE");
        }

        private TowOperator TowOperator(string name)
        {
            if (!_context.TowOperators.Any(x => x.Description == name))
            {
                return new TowOperator()
                {
                    Description = name,
                    TenantId = 1,
                    isActive = false
                };
            }
            else
                return new TowOperator() { };
        }

        /// <summary>
        /// CreateVendorMain
        /// </summary>
        public void CreateVendorMain()
        {
            CreateDefaultVendorMain();
        }
        private void CreateDefaultVendorMain()
        {
            var data = new List<VendorMain>();
            data.AddRange(GetDefaultVendorMains());
            _context.VendorMain.AddOrUpdate(data.ToArray());
            _context.SaveChanges();
        }
        private IEnumerable<VendorMain> GetDefaultVendorMains()
        {
            yield return VendorMain("OTHER");
            yield return VendorMain("NONE");
        }

        private VendorMain VendorMain(string name)
        {
            if (!_context.VendorMain.Any(x => x.SupplierName == name))
            {
                return new VendorMain()
                {
                    SupplierName = name,
                    SupplierCode = Guid.NewGuid()
                };
            }
            else
                return new VendorMain() { };
        }


        /// <summary>
        /// CreateVehicleMakes
        /// </summary>
        public void CreateVehicleMakes()
        {
            CreateDefaultVehicleMakes();
        }
        private void CreateDefaultVehicleMakes()
        {
            var data = new List<VehicleMake>();
            data.AddRange(GetDefaultVehicleMakess());
            _context.VehicleMake.AddOrUpdate(data.ToArray());
            _context.SaveChanges();
        }
        private IEnumerable<VehicleMake> GetDefaultVehicleMakess()
        {
            yield return VehicleMakes("OTHER");
            yield return VehicleMakes("NONE");
        }

        private VehicleMake VehicleMakes(string name)
        {
            if (!_context.VehicleMake.Any(x => x.Description == name))
            {
                return new VehicleMake()
                {
                    Description = name
                };
            }
            else
                return new VehicleMake() { };
        }

        /// <summary>
        /// CreateVehicleModels
        /// </summary>
        public void CreateVehicleModels()
        {
            CreateDefaultVehicleModels();
        }
        private void CreateDefaultVehicleModels()
        {
            var data = new List<VehicleModels>();
            data.AddRange(GetDefaultVehicleModelss());
            _context.VehicleModel.AddOrUpdate(data.ToArray());
            _context.SaveChanges();
        }
        private IEnumerable<VehicleModels> GetDefaultVehicleModelss()
        {
            yield return VehicleModels("OTHER",1,"123");
            yield return VehicleModels("NONE",1,"");

            yield return VehicleModels("OTHER", 2,"123");
            yield return VehicleModels("NONE", 2 , "");
        }

        private VehicleModels VehicleModels(string name, int vehicleId, string mmcode)
        {
            if (!_context.VehicleModel.Any(x => x.Model == name))
            {
                return new VehicleModels()
                {
                    Model = name ,
                    MMCode = mmcode,
                    VehicleMakeID = vehicleId
                };
            }
            else
                return new VehicleModels() { };
        }
    }
}
