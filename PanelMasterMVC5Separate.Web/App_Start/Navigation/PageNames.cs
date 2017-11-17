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
            }

            public static class Tenant
            {
                public const string Dashboard = "Dashboard.Tenant";
                public const string Quote = "Dashboard.Quote";
                public const string Quotations = "Quote.Quoting"; 

                public const string PartsOrdering = "Dashboard.PartsOrdering";

                public const string Claim = "Dashboard.Claim";
                public const string JobDetails = "Claim.JobDetails";
                public const string NewJob = "Claim.NewJob";
                public const string Productivity = "Dashboard.Productivity";
                public const string Towing = "Dashboard.Towing";
                public const string Costing = "Dashboard.Costing";
                public const string Settings = "Administration.Settings.Tenant";

                public const string Vendors = "Dashboard.Vendors";
                public const string VendorList = "Vendors.VendorList";
                public const string AddVendor = "Vendors.AddVendor";

                public const string Insurers = "Dashboard.Insurers";
                public const string InsurerList = "Insurers.Insurers";
                public const string AddInsurer = "Vendors.AddInsurer";

                public const string Brokers = "Dashboard.Brokers";
                public const string BrokerList = "Brokers.Brokers";
                public const string AddBroker = "Brokers.AddInsurer";

                public const string VehicleManufacturer = "Dashboard.VehicleManufacturer";
                public const string VehicleMakes = "Dashboard.VehicleMakes";
                public const string AddMake = "Dashboard.AddVehicleMake";
                public const string AddModel = "Dashboard.AddVehicleMade";
                public const string VehicleMades = "Dashboard.VehicleMades";

                public const string JobStatuses = "Administration.JobStatuses.Tenant";

                public const string TowOperators = "Administration.TowOperators.Tenant";

                public const string AdminFunctions = "Administration.AdminFunctions.Tenant";
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