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
using PanelMasterMVC5Separate.Estimations;
using PanelMasterMVC5Separate.Clients;
using PanelMasterMVC5Separate.Vendors;
using PanelMasterMVC5Separate.Insurer;
using PanelMasterMVC5Separate.Brokers;

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

        public virtual IDbSet<Estimator> Estimators { get; set; }

        public virtual IDbSet<Manufacture> Manufactures { get; set; }

        public virtual IDbSet<VehicleModel> VehicleModels { get; set; }

        public virtual IDbSet<BranchClaimStatus> ClaimStatus { get; set; }

        public virtual DbSet<Jobs> BranchClaims { get; set; }

        public virtual IDbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual IDbSet<Friendship> Friendships { get; set; }

        public virtual IDbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<VendorSub> VendorSub { get; set; }

        public virtual IDbSet<VendorMain> VendorMain { get; set; }

        public virtual IDbSet<Banks> Banks { get; set; }

        public virtual IDbSet<Currencies> Currencies { get; set; }

        public virtual IDbSet<InsurerMaster> InsurerMasters { get; set; }

        public virtual IDbSet<InsurerSub> InsurerSubs { get; set; }

        public virtual IDbSet<InsurerPics> InsurerPics { get; set; }

        public virtual IDbSet<BrokerMaster> BrokerMasters { get; set; }

        public virtual IDbSet<BrokerSubMaster> BrokerSubMasters { get; set; }

        public virtual IDbSet<BrokerMasterPics> BrokerMasterPics { get; set; }

        public virtual IDbSet<VehicleMake> VehicleMake { get; set; }
        public virtual IDbSet<VehicleModels> VehicleModel { get; set; }
        public virtual IDbSet<VehicleModelLogos> VehicleModelLogo { get; set; }

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
