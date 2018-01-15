using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using PanelMasterMVC5Separate.Authorization.Roles;
using PanelMasterMVC5Separate.Authorization.Claim;
using PanelMasterMVC5Separate.Chat;
using PanelMasterMVC5Separate.Friendships;
using PanelMasterMVC5Separate.MultiTenancy;
using PanelMasterMVC5Separate.Storage;
using PanelMasterMVC5Separate.Claim;
using PanelMasterMVC5Separate.Vehicle;
using PanelMasterMVC5Separate.Clients;
using PanelMasterMVC5Separate.Vendors;
using PanelMasterMVC5Separate.Insurer;
using PanelMasterMVC5Separate.Brokers;
using PanelMasterMVC5Separate.Quotings;
using PanelMasterMVC5Separate.RolesCategories;
using PanelMasterMVC5Separate.Migrations;

namespace PanelMasterMVC5Separate.EntityFramework
{
    /* Constructors of this DbContext is important and each one has it's own use case.
     * - Default constructor is used by EF tooling on design time.
     * - constructor(nameOrConnectionString) is used by ABP on runtime.
     * - constructor(existingConnection) is used by unit tests.
     * - constructor(existingConnection,contextOwnsConnection) can be used by ABP if DbContextEfTransactionStrategy is used.
     * See http://www.aspnetboilerplate.com/Pages/Documents/EntityFramework-Integration for more.
     */

    public class PanelMasterMVC5SeparateDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        /* Define an IDbSet for each entity of the application */


        public virtual IDbSet<Client> Clients { get; set; }
        public virtual DbSet<Jobs> BranchClaims { get; set; }
        public virtual IDbSet<BinaryObject> BinaryObjects { get; set; }
        public virtual IDbSet<Friendship> Friendships { get; set; }
        public virtual IDbSet<ChatMessage> ChatMessages { get; set; }
        public virtual DbSet<VendorSub> VendorSub { get; set; }
        public virtual IDbSet<VendorMain> VendorMain { get; set; }
        public virtual IDbSet<Countries> Countries { get; set; }
        public virtual IDbSet<CountryandCurrency> Currencies { get; set; }
        public virtual IDbSet<Banks> Banks { get; set; }
        public virtual IDbSet<InsurerMaster> InsurerMasters { get; set; }
        public virtual IDbSet<InsurerSub> InsurerSubs { get; set; }
        public virtual IDbSet<InsurerPics> InsurerPics { get; set; }
        public virtual IDbSet<BrokerMaster> BrokerMasters { get; set; }
        public virtual IDbSet<BrokerSubMaster> BrokerSubMasters { get; set; }
        public virtual IDbSet<BrokerMasterPics> BrokerMasterPics { get; set; }
        public virtual IDbSet<VehicleMake> VehicleMake { get; set; }
        public virtual IDbSet<VehicleModels> VehicleModel { get; set; }
        public virtual IDbSet<VehicleModelLogos> VehicleModelLogo { get; set; }
        public virtual IDbSet<SignonPlans> SignonPlan { get; set; }
        public virtual IDbSet<QuoteMaster> QuoteMasters { get; set; }
        public virtual IDbSet<RepairTypes> RepairTypes { get; set; }
        public virtual IDbSet<QuoteCategories> QuoteCategories { get; set; }
        public virtual IDbSet<QuoteStatus> QuoteStatus { get; set; }
        public virtual IDbSet<RolesCategory> RolesCategory { get; set; }
        public virtual IDbSet<NotProceedReason> NotProceedReason { get; set; }
        public virtual IDbSet<Jobstatus> Jobstatus { get; set; }
        public virtual IDbSet<JobstatusMask> JobstatusMask { get; set; }
        public virtual IDbSet<JobstatusTenant> JobstatusTenant { get; set; }
        public virtual IDbSet<TowOperator> TowOperators { get; set; }
        public virtual IDbSet<TowSubOperator> TowSubOperators { get; set; }
        public virtual IDbSet<TenantProfile> TenantProfiles { get; set; }
        public virtual IDbSet<TenantPlanBillingDetails> TenantPlanBillingDetail { get; set; }
        public virtual IDbSet<TenantCompanyLogo> TenantCompanyLogo { get; set; }
        public virtual IDbSet<PaintTypes> PaintTypes { get; set; }
        public virtual IDbSet<BrVehicle> BrVehicle { get; set; }
        public virtual IDbSet<VehicleInsurance> VehicleInsurance { get; set; }
        public virtual IDbSet<QPartType> QPartTypes { get; set; }
        public virtual IDbSet<QLocation> QLocations { get; set; }
        public virtual IDbSet<QAction> QActions { get; set; }
        public virtual IDbSet<QuoteDetails> QuoteDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
        }

        public PanelMasterMVC5SeparateDbContext()
            : base("Default")
        {

        }

        public PanelMasterMVC5SeparateDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public PanelMasterMVC5SeparateDbContext(DbConnection existingConnection)
           : base(existingConnection, false)
        {

        }

        public PanelMasterMVC5SeparateDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
