

namespace PanelMasterMVC5Separate.Web.Navigation
{
    public static class PageNames
    {
        public static class App
        {
            public static class Common
            {
                public const string Administration = "Administration";
                public const string Roles = "Administration.Roles";
                public const string Users = "Administration.Users";
                public const string AuditLogs = "Administration.AuditLogs";
                public const string OrganizationUnits = "Administration.OrganizationUnits";
                public const string Languages = "Administration.Languages";
            }

            public static class Host
            {
                public const string Tenants = "Tenants";
                public const string Editions = "Editions";
                public const string Maintenance = "Administration.Maintenance";
                public const string Settings = "Administration.Settings.Host";
                public const string SystemDefaults = "Administration.SystemDefaults";
                public static string Banks = "Administration.SystemDefaults.Banks";
                public static string AddBank = "Banks.AddBank";
                public const string VehicleMakes = "Administration.SystemDefaults.VehicleMakes";
                public const string AddMake = "VehicleMakes.AddVehicleMake";
                public const string VehicleMades = "Administration.SystemDefaults.VehicleMades";
                public const string AddModel = "VehicleMades.AddVehicleMade";
                public const string Insurers = "Administration.SystemDefaults.Insurers";
                public const string InsurerList = "Insurers.Insurers";
                public const string AddInsurer = "Insurers.AddInsurer";
                public const string Brokers = "Administration.SystemDefaults.Brokers";
                public const string BrokerList = "Brokers.Brokers";
                public const string AddBroker = "Brokers.AddBroker";
                public const string MainVendors = "Administration.SystemDefaults.Vendors";
                public const string Vendors = "MainVendors.Vendors";
                public const string AddVendor = "MainVendors.AddVendor";
                public const string TowOperators = "SystemDefaults.TowOperators.Host";
                public const string JobStatuses = "SystemDefaults.JobStatuses.Host";
                public const string JobMaskStatuses = "SystemDefaults.JobMaskStatuses.Host";
                public static string QuoteStatus = "SystemDefaults.QuoteStatus.Host";
                public static string RepairTypes = "SystemDefaults.RepairTypes.Host";
                public static string RoleCategories = "SystemDefaults.RoleCategories.Host";
                public static string SignonPlans = "SystemDefaults.SignonPlans.Host";
            }

            public static class Tenant
            {
                public const string Dashboard = "Dashboard.Tenant";
                public const string Quote = "Dashboard.Quote";
                public const string Quotations = "Quote.Quoting";

                public const string ManageRepairs = "Dashboard.ManageRepairs";
                public const string Repairs = "ManageRepairs.ManageRepairs";
                public const string CreateRepair = "ManageRepairs.CreateRepair";
                public const string Calender = "ManageRepairs.Calender";

                public const string PartsOrdering = "Dashboard.PartsOrdering";
                public const string GoodReceived = "PartsOrdering.GoodReceived";
                public const string CreditNotes = "PartsOrdering.CreditNotes";

                public const string Claim = "Dashboard.Claim";
                public const string JobDetails = "Claim.JobDetails";
                public const string NewJob = "Claim.NewJob";
                public const string Productivity = "Dashboard.Productivity";
                public const string Towing = "Dashboard.Towing";

                public const string Financials = "Dashboard.Financials";
                public const string Invoicing = "Financials.Invoicing";
                public const string Costing = "Financials.Costing";

                public const string Settings = "Administration.Settings.Tenant";

                public const string Vendors = "Dashboard.Vendors";
                public const string VendorList = "Vendors.VendorList";
                public const string AddVendor = "Vendors.AddVendor";

                public const string Insurers = "Dashboard.Insurers";
                public const string InsurerList = "Insurers.Insurers";

                public const string Brokers = "Dashboard.Brokers";
                public const string BrokerList = "Brokers.Brokers";

                public const string JobStatuses = "Administration.JobStatuses.Tenant";
                public const string JobMaskStatuses = "Administration.JobMaskStatuses.Tenant";
                public const string TowOperators = "Administration.TowOperators.Tenant";

                public const string Tenants = "Dashboard.Tenants";

                public const string CreateJob = "Administration.CreateJob.Tenant";

                // public const string VehicleManufacturer = "Dashboard.VehicleManufacturer";                 
            }
        }

        public static class Frontend
        {
            public const string Home = "Frontend.Home";
            public const string About = "Frontend.About";
        }
    }
}