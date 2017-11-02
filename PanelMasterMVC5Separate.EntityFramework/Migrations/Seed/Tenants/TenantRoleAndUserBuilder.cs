using System.Linq;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using PanelMasterMVC5Separate.Authorization;
using PanelMasterMVC5Separate.Authorization.Roles;
using PanelMasterMVC5Separate.Authorization.Claim;
using PanelMasterMVC5Separate.EntityFramework;

namespace PanelMasterMVC5Separate.Migrations.Seed.Tenants
{
    public class TenantRoleAndUserBuilder
    {
        private readonly PanelMasterMVC5SeparateDbContext _context;
        private readonly int _tenantId;

        public TenantRoleAndUserBuilder(PanelMasterMVC5SeparateDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            //Admin role

            var adminRole = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Admin);
            if (adminRole == null)
            {
                adminRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Admin, StaticRoleNames.Tenants.Admin) { IsStatic = true, RoleCategoryID = 1 });
                _context.SaveChanges();

                //Grant all permissions to admin role
                var permissions = PermissionFinder
                    .GetAllPermissions(new AppAuthorizationProvider(false))
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant))
                    .ToList();

                foreach (var permission in permissions)
                {
                    _context.Permissions.Add(
                        new RolePermissionSetting
                        {
                            TenantId = _tenantId,
                            Name = permission.Name,
                            IsGranted = true,
                            RoleId = adminRole.Id
                        });
                }

                _context.SaveChanges();
            }

            //User role

            var userRole = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.User);
            if (userRole == null)
            {
                _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.User, StaticRoleNames.Tenants.User) { IsStatic = true, IsDefault = true, RoleCategoryID = 2 });
                _context.SaveChanges();
            }

            var Claims_HandlerRole = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Claims_Handler);
            if (Claims_HandlerRole == null)
            {
                _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Claims_Handler, StaticRoleNames.Tenants.Claims_Handler) { IsStatic = true, IsDefault = false, RoleCategoryID = 3 });
                _context.SaveChanges();
            }

            var CSARole = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.CSA);
            if (CSARole == null)
            {
                _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.CSA, StaticRoleNames.Tenants.CSA) { IsStatic = true, IsDefault = false, RoleCategoryID = 4 });
                _context.SaveChanges();
            }

            var PartsBuyerRole = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Parts_Buyer);
            if (PartsBuyerRole == null)
            {
                _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Parts_Buyer, StaticRoleNames.Tenants.Parts_Buyer) { IsStatic = true, IsDefault = false, RoleCategoryID = 5 });
                _context.SaveChanges();
            }

            var EstimatorRole = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Estimator);
            if (EstimatorRole == null)
            {
                _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Estimator, StaticRoleNames.Tenants.Estimator) { IsStatic = true, IsDefault = false, RoleCategoryID = 6 });
                _context.SaveChanges();
            }

            var KeyAccountManagerRole = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Key_Accounts_Manager);
            if (KeyAccountManagerRole == null)
            {
                _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Key_Accounts_Manager, StaticRoleNames.Tenants.Key_Accounts_Manager) { IsStatic = true, IsDefault = false, RoleCategoryID = 7 });
                _context.SaveChanges();
            }

            var SwithchboardRole = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Swithchboard);
            if (SwithchboardRole == null)
            {
                _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Swithchboard, StaticRoleNames.Tenants.Swithchboard) { IsStatic = true, IsDefault = false, RoleCategoryID = 8 });
                _context.SaveChanges();
            }
           
            var PartsReceiverRole = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Parts_Receiver);
            if (PartsReceiverRole == null)
            {
                _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Parts_Receiver, StaticRoleNames.Tenants.Parts_Receiver) { IsStatic = true, IsDefault = false, RoleCategoryID = 9 });
                _context.SaveChanges();
            }

            var CostingClerkRole = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Costing_Clerk);
            if (CostingClerkRole == null)
            {
                _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Costing_Clerk, StaticRoleNames.Tenants.Costing_Clerk) { IsStatic = true, IsDefault = false, RoleCategoryID = 10 });
                _context.SaveChanges();
            }

            var FinancialManagerRole = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Financial_Manager);
            if (FinancialManagerRole == null)
            {
                _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Financial_Manager, StaticRoleNames.Tenants.Financial_Manager) { IsStatic = true, IsDefault = false, RoleCategoryID = 11 });
                _context.SaveChanges();
            }

            var InsurerRole = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Insurer);
            if (InsurerRole == null)
            {
                _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Insurer, StaticRoleNames.Tenants.Insurer) { IsStatic = true, IsDefault = false, RoleCategoryID = 12 });
                _context.SaveChanges();
            }

            var BrokerRole = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Broker);
            if (BrokerRole == null)
            {
                _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Broker, StaticRoleNames.Tenants.Broker) { IsStatic = true, IsDefault = false, RoleCategoryID = 13 });
                _context.SaveChanges();
            }

            //admin user

            var adminUser = _context.Users.FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == User.AdminUserName);
            if (adminUser == null)
            {
                adminUser = User.CreateTenantAdminUser(_tenantId, "admin@defaulttenant.com", "123qwe");
                adminUser.IsEmailConfirmed = true;
                adminUser.ShouldChangePasswordOnNextLogin = true;
                adminUser.IsActive = true;

                _context.Users.Add(adminUser);
                _context.SaveChanges();

                //Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(_tenantId, adminUser.Id, adminRole.Id));
                _context.SaveChanges();

                //User account of admin user
                if (_tenantId == 1)
                {
                    _context.UserAccounts.Add(new UserAccount
                    {
                        TenantId = _tenantId,
                        UserId = adminUser.Id,
                        UserName = User.AdminUserName,
                        EmailAddress = adminUser.EmailAddress
                    });
                    _context.SaveChanges();
                }
            }
        }
    }
}
