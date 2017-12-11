using PanelMasterMVC5Separate.Brokers;
using PanelMasterMVC5Separate.Claim;
using PanelMasterMVC5Separate.EntityFramework;
using PanelMasterMVC5Separate.Insurer;
using PanelMasterMVC5Separate.MultiTenancy;
using PanelMasterMVC5Separate.Quotings;
using PanelMasterMVC5Separate.Vehicle;
using PanelMasterMVC5Separate.Vendors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;

namespace MasterData
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new PanelMasterMVC5SeparateDbContext("Data Source=localhost;Initial Catalog=PanelMasterMVC5Separate;Integrated Security=True");

            System.Console.WriteLine("Would you like to add master data to tblJobstatus? Y/N");
            var anwser = System.Console.ReadLine();
            if (anwser?.ToLower() == "Y".ToLower())
                AddJobstatus(context);

            System.Console.WriteLine("Would you like to add master data to CreateDefaultVehicleMakes? Y /N");
            anwser = System.Console.ReadLine();
            if (anwser?.ToLower() == "Y".ToLower())
                CreateDefaultVehicleMakes(context);

            System.Console.WriteLine("Would you like to add master data to CreateDefaultVehicleModels? Y /N");
            anwser = System.Console.ReadLine();
            if (anwser?.ToLower() == "Y".ToLower())
                CreateDefaultVehicleModels(context);

            System.Console.WriteLine("Would you like to add master data to CreateDefaultVendorMain? Y /N");
            anwser = System.Console.ReadLine();
            if (anwser?.ToLower() == "Y".ToLower())
                CreateDefaultVendorMain(context);

            System.Console.WriteLine("Would you like to add master data to tblInsurerMaster? Y /N");
            anwser = System.Console.ReadLine();
            if (anwser?.ToLower() == "Y".ToLower())
                AddInsurerMaster(context);

            System.Console.WriteLine("Would you like to add master data to CreateBrokerMaster? Y /N");
            anwser = System.Console.ReadLine();
            if (anwser?.ToLower() == "Y".ToLower())
                CreateBrokerMaster(context);

            //System.Console.WriteLine("Would you like to add master data to tblCountries? Y /N");
            //anwser = System.Console.ReadLine();
            //if (anwser?.ToLower() == "Y".ToLower())
            //    AddCountries();

            System.Console.WriteLine("Would you like to add master data to CreateDefaultBanks ? Y /N");
            anwser = System.Console.ReadLine();
            if (anwser?.ToLower() == "Y".ToLower())
                CreateDefaultBanks(context);

            System.Console.WriteLine("Would you like to add master data to tblTowOperator? Y /N");
            anwser = System.Console.ReadLine();
            if (anwser?.ToLower() == "Y".ToLower())
                CreateDefaultTowOperator(context);

            System.Console.WriteLine("Would you like to add master data to tblJobstatusMask? Y /N");
            anwser = System.Console.ReadLine();
            if (anwser?.ToLower() == "Y".ToLower())
                AddJobstatusMask(context);

            System.Console.WriteLine("Would you like to create for tblNotProceedReason? Y/N");
            anwser = System.Console.ReadLine();

            if (anwser?.ToLower() == "Y".ToLower())
                NotProceedReason(context);

            System.Console.WriteLine("Would you like to create master SignOnPlans? Y/N");
            anwser = System.Console.ReadLine();

            if (anwser?.ToLower() == "Y".ToLower())
                SignOnPlans(context);

            System.Console.WriteLine("Would you like to create master Quote Status? Y/N");
            anwser = System.Console.ReadLine();

            if (anwser?.ToLower() == "Y".ToLower())
                AddQuoteStatus(context);

            System.Console.WriteLine("Would you like to create master repair types? Y/N");
            anwser = System.Console.ReadLine();

            if (anwser?.ToLower() == "Y".ToLower())
                AddRepairTypes(context);

            System.Console.WriteLine("Would you like to create master quote categories? Y/N");
            anwser = System.Console.ReadLine();

            if (anwser?.ToLower() == "Y".ToLower())
                AddQuoteCategories(context);

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                DisplayMessage(e.Message);
            }

            System.Console.WriteLine("complete");
            System.Console.ReadLine();
        }

        private static void CreateDefaultVendorMain(PanelMasterMVC5SeparateDbContext _context)
        {
            var data = new List<VendorMain>();
            data.AddRange(GetDefaultVendorMains());
            _context.VendorMain.AddOrUpdate(data.ToArray());
            _context.SaveChanges();
        }
        private static IEnumerable<VendorMain> GetDefaultVendorMains()
        {
            yield return VendorMain("OTHER");
            yield return VendorMain("NONE");
        }

        private static VendorMain VendorMain(string name)
        {
            return new VendorMain()
            {
                SupplierName = name,
                SupplierCode = Guid.NewGuid(),
                CountryID = 250,
                IsDeleted = false,
                CreationTime = DateTime.Now
            };
        }

        private static void CreateDefaultVehicleMakes(PanelMasterMVC5SeparateDbContext _context)
        {
            var data = new List<VehicleMake>();
            data.AddRange(GetDefaultVehicleMakess());
            _context.VehicleMake.AddOrUpdate(data.ToArray());
            _context.SaveChanges();


            var pics = new List<VehicleModelLogos>();
            var Ids = (from f in data
                       select new VehicleMake()
                       { Id = f.Id, LogoPicture = f.LogoPicture }).ToList();

            pics.AddRange(GetDefaultVehicleMakeLogos(Ids));
            _context.VehicleModelLogo.AddOrUpdate(pics.ToArray());
            _context.SaveChanges();
        }

        private static IEnumerable<VehicleModelLogos> GetDefaultVehicleMakeLogos(List<VehicleMake> ids)
        {
            foreach (var a in ids)
            {
                yield return VehicleModelLogos(a.Id, a.LogoPicture);
            }
        }
        private static VehicleModelLogos VehicleModelLogos(int id, string LogoPicture)
        {
            return new VehicleModelLogos()
            {
                VehicleMakeID = id,
                Bytes = GetBytes(LogoPicture),
                IsDeleted = false,
                CreationTime = DateTime.Now
            };
        }
        private static IEnumerable<VehicleMake> GetDefaultVehicleMakess()
        {
            yield return VehicleMakes("OTHER");
            yield return VehicleMakes("NONE");
        }

        private static VehicleMake VehicleMakes(string name)
        {
            return new VehicleMake()
            {
                Description = name,
                LogoPicture = "default-profile-picture.png",
                IsDeleted = false,
                CreationTime = DateTime.Now
            };
        }

        private static void CreateDefaultVehicleModels(PanelMasterMVC5SeparateDbContext _context)
        {
            var data = new List<VehicleModels>();
            data.AddRange(GetDefaultVehicleModelss());
            _context.VehicleModel.AddOrUpdate(data.ToArray());
            _context.SaveChanges();

        }
        private static IEnumerable<VehicleModels> GetDefaultVehicleModelss()
        {
            yield return VehicleModels("OTHER", 1, "123");
            yield return VehicleModels("NONE", 1, "NONE");

            yield return VehicleModels("OTHER", 2, "123");
            yield return VehicleModels("NONE", 2, "NONE");
        }

        private static VehicleModels VehicleModels(string name, int vehicleId, string mmcode)
        {
            return new VehicleModels()
            {
                Model = name,
                MMCode = mmcode,
                VehicleMakeID = vehicleId,
                IsDeleted = false,
                CreationTime = DateTime.Now
            };
        }
        private static void CreateDefaultTowOperator(PanelMasterMVC5SeparateDbContext _context)
        {
            var data = new List<TowOperator>();
            data.AddRange(GetDefaultTowOperators());
            _context.TowOperators.AddOrUpdate(data.ToArray());
            _context.SaveChanges();
        }
        private static IEnumerable<TowOperator> GetDefaultTowOperators()
        {
            yield return TowOperator("OTHER");
            yield return TowOperator("NONE");
        }

        private static TowOperator TowOperator(string name)
        {
            return new TowOperator()
            {
                Description = name,            
                isActive = false,
                CountryID = 250,
                CreationTime = DateTime.Now
            };
        }
        private static void CreateBrokerMaster(PanelMasterMVC5SeparateDbContext _context)
        {
            var data = new List<BrokerMaster>();
            data.AddRange(GetDefaultBrokerMasters());
            _context.BrokerMasters.AddOrUpdate(data.ToArray());
            _context.SaveChanges();

            var pics = new List<BrokerMasterPics>();
            var Ids = (from f in data
                       select new BrokerMaster()
                       { Id = f.Id, LogoPicture = f.LogoPicture }).ToList();

            pics.AddRange(GetDefaultBrokerMastersPics(Ids));
            _context.BrokerMasterPics.AddOrUpdate(pics.ToArray());
            _context.SaveChanges();
        }

        private static IEnumerable<BrokerMasterPics> GetDefaultBrokerMastersPics(List<BrokerMaster> ids)
        {
            foreach (var a in ids)
            {
                yield return BrokerMasterPics(a.Id, a.LogoPicture);
            }
        }
        private static BrokerMasterPics BrokerMasterPics(int id, string LogoPicture)
        {
            return new BrokerMasterPics()
            {
                BrokerID = id,
                Bytes = GetBytes(LogoPicture),
                IsDeleted = false,
                CreationTime = DateTime.Now
            };
        }
        private static IEnumerable<BrokerMaster> GetDefaultBrokerMasters()
        {
            yield return BrokerMaster("OTHER", "2132");
            yield return BrokerMaster("NONE", "NONE");
        }

        private static BrokerMaster BrokerMaster(string name, string mask)
        {
            return new BrokerMaster()
            {
                BrokerName = name,
                LogoPicture = "default-profile-picture.png",
                Mask = mask,                
                IsDeleted = false,
                CreationTime = DateTime.Now,
                CountryID = 250
            };
        }
        private static void CreateDefaultBanks(PanelMasterMVC5SeparateDbContext _context)
        {
            var data = new List<Banks>();
            data.AddRange(GetDefaultBanks());
            _context.Banks.AddOrUpdate(data.ToArray());
            _context.SaveChanges();
        }
        private static IEnumerable<Banks> GetDefaultBanks()
        {
            yield return Banks("OTHER");
            yield return Banks("NONE");
        }

        private static Banks Banks(string name)
        {
            return new Banks()
            {
                BankName = name,
                IsDeleted = false,
                CreationTime = DateTime.Now,
                CountryID = 250
            };
        }
        private static void AddInsurerMaster(PanelMasterMVC5SeparateDbContext _context)
        {
            var data = new List<InsurerMaster>();
            data.AddRange(GetDefaultInsurerMasters());
            _context.InsurerMasters.AddOrUpdate(data.ToArray());
            _context.SaveChanges();

            var pics = new List<InsurerPics>();
            var Ids = (from f in data
                      select new InsurerMaster()
                      { Id = f.Id, LogoPicture = f.LogoPicture }).ToList();

            pics.AddRange(GetDefaultInsurerMastersPics(Ids));
            _context.InsurerPics.AddOrUpdate(pics.ToArray());
            _context.SaveChanges();
        }

        private static IEnumerable<InsurerPics> GetDefaultInsurerMastersPics(List<InsurerMaster> ids)
        {
            foreach (var a in ids)
            {
                yield return InsurerPics(a.Id, a.LogoPicture);
            }
        }
        private static InsurerPics InsurerPics(int id ,string LogoPicture)
        {
            return new InsurerPics()
            {
                InsurerID = id,
                Bytes  = GetBytes(LogoPicture),
                IsDeleted = false,
                CreationTime = DateTime.Now
            };
        }
        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        private static IEnumerable<InsurerMaster> GetDefaultInsurerMasters()
        {
            yield return InsurerMaster("OTHER");
            yield return InsurerMaster("NONE");
        }

        private static InsurerMaster InsurerMaster(string name)
        {
            return new InsurerMaster()
            {
                InsurerName = name,
                LogoPicture = "default-profile-picture.png",
                Mask = name,
                IsDeleted = false,
                CountryID = 250,
                CreationTime = DateTime.Now
            };
        }

        private static void AddCountries()
        {
            string File = @"F:\Projects\Hennie\Code\MasterData\MilestoneEstimationlocalization.xlsx";
            // Connection String to Excel Workbook

            string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", File);

            OleDbConnection connection = new OleDbConnection();

            connection.ConnectionString = excelConnectionString;

            connection.Open();
            string sheet1 = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();

            DataTable dtExcelData = new DataTable();

            using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "]", connection))
            {
                oda.Fill(dtExcelData);
            }
            connection.Close();

            DataTable dtExcelData1 = new DataTable();
            dtExcelData1.Columns.AddRange(new DataColumn[4] {

                new DataColumn("Code", typeof(string)),
                new DataColumn("Country",typeof(string)),
                new DataColumn("IsDeleted",typeof(bool)),
                new DataColumn("CreationTime",typeof(DateTime))
            });

            string[] str = null;
            DataRow dc = null;
            for (int i = 0; i < dtExcelData.Rows.Count; i++)
            {
                str = dtExcelData.Rows[i][0].ToString().Split(':');
                dc = dtExcelData1.NewRow();
                dc["Code"] = str[0];
                dc["Country"] = str[1];
                dc["IsDeleted"] = false;
                dc["CreationTime"] = DateTime.Now;
                dtExcelData1.Rows.Add(dc);
            }

            string sqlConnectionString = @"Data Source=localhost;Initial Catalog=PanelMasterMVC5Separate;Integrated Security=True";
            SqlConnection con = new SqlConnection(sqlConnectionString);

            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            objbulk.DestinationTableName = "tblCoutries";
            SqlBulkCopyColumnMapping a = new SqlBulkCopyColumnMapping();

            objbulk.ColumnMappings.Add("Code", "Code");
            objbulk.ColumnMappings.Add("Country", "Country");
            objbulk.ColumnMappings.Add("IsDeleted", "IsDeleted");
            objbulk.ColumnMappings.Add("CreationTime", "CreationTime");

            con.Open();
            objbulk.WriteToServer(dtExcelData1);
            con.Close();

        }

        private static void AddTowOperator(PanelMasterMVC5SeparateDbContext context)
        {
            var defaultTenant = context.Tenants.Select(x => x.Id).ToList();
            var data = new List<TowOperator>();
            // data.AddRange(GetTowOperators(1));
            foreach (var s in defaultTenant)
            {
                data.AddRange(GetTowOperators(s, context));
            }
            context.TowOperators.AddOrUpdate(data.ToArray());
        }
        private static IEnumerable<TowOperator> GetTowOperators(int tenantId, PanelMasterMVC5SeparateDbContext context)
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
                yield return GetTowOperator(str[i], tenantId);
            }
        }
        private static TowOperator GetTowOperator(string desc, int tenantId)
        {
            return new TowOperator()
            {
                Description = desc,                 
                isActive = false,
                IsDeleted = false,                
                CreationTime = DateTime.Now,
                CountryID = 250
            };
        }
        private static void AddJobstatusMask(PanelMasterMVC5SeparateDbContext context)
        {
            var data = new List<JobstatusMask>();

            data.AddRange(GetJobStatusMask());

            context.JobstatusMask.AddOrUpdate(data.ToArray());
        }

        private static IEnumerable<JobstatusMask> GetJobStatusMask()
        {
            string allstatus = "Quoting,Authorised,Conversion,Work In Progress,Delivery,Invoicing,Completed,Other";
            string[] str = allstatus.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                yield return JobstatusMask(str[i]);
            }
        }
        private static JobstatusMask JobstatusMask(string desc)
        {
            return new JobstatusMask()
            {
                Description1 = desc,
                IsDeleted = false,
                CreationTime = DateTime.Now
            };
        }
        private static void AddJobstatus(PanelMasterMVC5SeparateDbContext context)
        {
            var data = new List<Jobstatus>();

            data.AddRange(GetStatuses());

            context.Jobstatus.AddOrUpdate(data.ToArray());
        }

        private static IEnumerable<Jobstatus> GetStatuses()
        {
            string allstatus = "Quoting,Repair Authorised,Parts Confirmation/ Pre Order,Repair Booked,Missed Appointment,Checked in On Site," +
                "Awaiting Additionals Authorization,Converted,Parts Ordering,Parts Received,Express Repair,Panelbeating,Paint Preparation,Painting," +
                "Assembly,Polishing,Cleaning,Quality Control,Ready,Collection Booked,Collection Missed Appointment,Delivered,Final Costing,Invoiced," +
                "Payment Received,Closed,Not Proceeding";
            string[] str = allstatus.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                yield return Jobstatus(str[i]);
            }
        }

        private static Jobstatus Jobstatus(string desc)
        {
            return new Jobstatus()
            {
                Description = desc,
                IsDeleted = false,
                CreationTime = DateTime.Now
            };
        }
        private static void NotProceedReason(PanelMasterMVC5SeparateDbContext context)
        {
            var data = new List<NotProceedReason>();
            data.AddRange(GetNotProceedReason());
            context.NotProceedReason.AddOrUpdate(data.ToArray());
        }

        private static IEnumerable<NotProceedReason> GetNotProceedReason()
        {
            yield return NotProceedReasons("Vehicle Written Off");
            yield return NotProceedReasons("Assessor sent job elsewhere");
            yield return NotProceedReasons("Insurer / Broker sent job elsewhere");
            yield return NotProceedReasons("Insurer rejected claim");
            yield return NotProceedReasons("Client prefer to repair elsewhere");
            yield return NotProceedReasons("Client withdrew Claim");
            yield return NotProceedReasons("Funding - No Excess Money");
            yield return NotProceedReasons("Funding - No Money to Repair");
            yield return NotProceedReasons("Funding - Quote too Expensive");
            yield return NotProceedReasons("Vehicle sold before repair");
            yield return NotProceedReasons("Wrong Geographic location");
            yield return NotProceedReasons("No Manufacturer Approvals");
            yield return NotProceedReasons("Parts not Available");
            yield return NotProceedReasons("Duplicate Job");
            yield return NotProceedReasons("Other");
        }

        private static NotProceedReason NotProceedReasons(string desc)
        {
            return new NotProceedReason()
            {
                Description = desc,
                IsDeleted = false,
                CreationTime = DateTime.Now
            };
        }

        private static void SignOnPlans(PanelMasterMVC5SeparateDbContext context)
        {
            var data = new List<SignonPlans>();
            data.AddRange(GetPlans());
            context.SignonPlan.AddOrUpdate(data.ToArray());
        }
        private static IEnumerable<SignonPlans> GetPlans()
        {
            yield return SignonPlans("Bugdet", 24.00, "blue", 3);
            yield return SignonPlans("Solo", 39.00, "red", 5);
            yield return SignonPlans("Start Up", 59.00, "green", 20);
            yield return SignonPlans("Enterprise", 128.00, "purple", 100);
        }
        public static SignonPlans SignonPlans(string planname, double price, string color, int member)
        {
            return new SignonPlans()
            {
                PlanName = planname,
                Price = price,
                HeaderColor = color,
                Members = member,
                IsDeleted = false,
                CreationTime = DateTime.Now
            };
        }

        //Master Data entry : QuoteStatus
        private static void AddQuoteStatus(PanelMasterMVC5SeparateDbContext context)
        {
            var data = new List<QuoteStatus>();
            data.AddRange(GetQuoteStatus());
            context.QuoteStatus.AddOrUpdate(data.ToArray());
        }
        private static IEnumerable<QuoteStatus> GetQuoteStatus()
        {
            yield return QuoteStatus("Quote Preparation");
            yield return QuoteStatus("Quote Send - Awating Authorization");
            yield return QuoteStatus("Quote Approved");
            yield return QuoteStatus("Quote Rejected");
            yield return QuoteStatus("Closed");

        }
        public static QuoteStatus QuoteStatus(string description)
        {
            return new QuoteStatus()
            {
                Enabled = true,
                Description = description,
                IsDeleted = false,
                CreationTime = DateTime.Now
            };
        }

        //Master Data entry : QuoteCategories
        private static void AddQuoteCategories(PanelMasterMVC5SeparateDbContext context)
        {
            var data = new List<QuoteCategories>();
            data.AddRange(GetQuoteCategories());
            context.QuoteCategories.AddOrUpdate(data.ToArray());
        }
        private static IEnumerable<QuoteCategories> GetQuoteCategories()
        {
            yield return QuoteCategories("Initial Repair");
            yield return QuoteCategories("Upsell");

        }
        public static QuoteCategories QuoteCategories(string description)
        {
            return new QuoteCategories()
            {
                Enabled = true,
                Description = description,
                IsDeleted = false,
                CreationTime = DateTime.Now
            };
        }


        //Master Data entry : RepairTypes
        private static void AddRepairTypes(PanelMasterMVC5SeparateDbContext context)
        {
            var data = new List<RepairTypes>();
            data.AddRange(GetRepairTypes());
            context.RepairTypes.AddOrUpdate(data.ToArray());
        }
        private static IEnumerable<RepairTypes> GetRepairTypes()
        {
            yield return RepairTypes("Cash");
            yield return RepairTypes("Insured");
            yield return RepairTypes("Warantee");
        }
        public static RepairTypes RepairTypes(string description)
        {
            return new RepairTypes()
            {
                Enabled = true,
                Description = description,
                IsDeleted = false,
                CreationTime = DateTime.Now
            };
        }

        private static void DisplayMessage(string message)
        {
            System.Console.WriteLine();
            System.Console.ReadLine();
        }
    }
}
