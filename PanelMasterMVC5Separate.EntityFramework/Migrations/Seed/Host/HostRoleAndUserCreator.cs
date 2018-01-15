using System.Linq;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using PanelMasterMVC5Separate.Authorization;
using PanelMasterMVC5Separate.Authorization.Roles;
using PanelMasterMVC5Separate.Authorization.Claim;
using PanelMasterMVC5Separate.EntityFramework;

namespace PanelMasterMVC5Separate.Migrations.Seed.Host
{
    public class HostRoleAndUserCreator
    {
        private readonly PanelMasterMVC5SeparateDbContext _context;

        public HostRoleAndUserCreator(PanelMasterMVC5SeparateDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateHostRoleAndUsers();
            CreateRolesCategory();
        }

        private void CreateRolesCategory()
        {          
            var AdminRoleDesc = _context.RolesCategory.FirstOrDefault(r => r.Description.ToLower() == StaticRoleNames.Tenants.Admin.ToLower());
            if(AdminRoleDesc == null)
            {
                AdminRoleDesc = _context.RolesCategory.Add(
                      new RolesCategories.RolesCategory{
                          Description = StaticRoleNames.Tenants.Admin,
                          Enabled = true
                      });
                          
                _context.SaveChanges();
            }

            var UserRoleDesc = _context.RolesCategory.FirstOrDefault(r => r.Description.ToLower() == StaticRoleNames.Tenants.User.ToLower());
            if (UserRoleDesc == null)
            {
                UserRoleDesc = _context.RolesCategory.Add(
                      new RolesCategories.RolesCategory
                      {
                          Description = StaticRoleNames.Tenants.User,
                          Enabled = true
                      });

                _context.SaveChanges();
            }

            var ClaimHandlerRoleDesc = _context.RolesCategory.FirstOrDefault(r => r.Description.ToLower() == StaticRoleNames.Tenants.Claims_Handler.ToLower());
            if (ClaimHandlerRoleDesc == null)
            {
                ClaimHandlerRoleDesc = _context.RolesCategory.Add(
                      new RolesCategories.RolesCategory
                      {
                          Description = StaticRoleNames.Tenants.Claims_Handler,
                          Enabled = true                        
                          
                      });

                _context.SaveChanges();
            }

            var CSARoleDesc = _context.RolesCategory.FirstOrDefault(r => r.Description.ToLower() == StaticRoleNames.Tenants.CSA.ToLower());
            if (CSARoleDesc == null)
            {
                CSARoleDesc = _context.RolesCategory.Add(
                      new RolesCategories.RolesCategory
                      {
                          Description = StaticRoleNames.Tenants.CSA,
                          Enabled = true
                      });

                _context.SaveChanges();
            }

            var PartsBuyerRoleDesc = _context.RolesCategory.FirstOrDefault(r => r.Description.ToLower() == StaticRoleNames.Tenants.Parts_Buyer.ToLower());
            if (PartsBuyerRoleDesc == null)
            {
                PartsBuyerRoleDesc = _context.RolesCategory.Add(
                      new RolesCategories.RolesCategory
                      {
                          Description = StaticRoleNames.Tenants.Parts_Buyer,
                          Enabled = true
                      });

                _context.SaveChanges();
            }

            var EstimatorRoleDesc = _context.RolesCategory.FirstOrDefault(r => r.Description.ToLower() == StaticRoleNames.Tenants.Estimator.ToLower());
            if (EstimatorRoleDesc == null)
            {
                EstimatorRoleDesc = _context.RolesCategory.Add(
                      new RolesCategories.RolesCategory
                      {
                          Description = StaticRoleNames.Tenants.Estimator,
                          Enabled = true
                      });

                _context.SaveChanges();
            }

            var KeyAccountsManagerRoleDesc = _context.RolesCategory.FirstOrDefault(r => r.Description.ToLower() == StaticRoleNames.Tenants.Key_Accounts_Manager.ToLower());
            if (KeyAccountsManagerRoleDesc == null)
            {
                KeyAccountsManagerRoleDesc = _context.RolesCategory.Add(
                      new RolesCategories.RolesCategory
                      {
                          Description = StaticRoleNames.Tenants.Key_Accounts_Manager,
                          Enabled = true
                      });

                _context.SaveChanges();
            }

            var SwithchboardRoleDesc = _context.RolesCategory.FirstOrDefault(r => r.Description.ToLower() == StaticRoleNames.Tenants.Swithchboard.ToLower());
            if (SwithchboardRoleDesc == null)
            {
                SwithchboardRoleDesc = _context.RolesCategory.Add(
                      new RolesCategories.RolesCategory
                      {
                          Description = StaticRoleNames.Tenants.Swithchboard,
                          Enabled = true
                      });

                _context.SaveChanges();
            }

            var PartsReceiverRoleDesc = _context.RolesCategory.FirstOrDefault(r => r.Description.ToLower() == StaticRoleNames.Tenants.Parts_Receiver.ToLower());
            if (PartsReceiverRoleDesc == null)
            {
                PartsReceiverRoleDesc = _context.RolesCategory.Add(
                      new RolesCategories.RolesCategory
                      {
                          Description = StaticRoleNames.Tenants.Parts_Receiver,
                          Enabled = true
                      });

                _context.SaveChanges();
            }

            var CostingClerkRoleDesc = _context.RolesCategory.FirstOrDefault(r => r.Description.ToLower() == StaticRoleNames.Tenants.Costing_Clerk.ToLower());
            if (CostingClerkRoleDesc == null)
            {
                CostingClerkRoleDesc = _context.RolesCategory.Add(
                      new RolesCategories.RolesCategory
                      {
                          Description = StaticRoleNames.Tenants.Costing_Clerk,
                          Enabled = true
                      });

                _context.SaveChanges();
            }

            var FinancialManagerRoleDesc = _context.RolesCategory.FirstOrDefault(r => r.Description.ToLower() == StaticRoleNames.Tenants.Financial_Manager.ToLower());
            if (FinancialManagerRoleDesc == null)
            {
                FinancialManagerRoleDesc = _context.RolesCategory.Add(
                      new RolesCategories.RolesCategory
                      {
                          Description = StaticRoleNames.Tenants.Financial_Manager,
                          Enabled = true
                      });

                _context.SaveChanges();
            }

            var InsurerRoleDesc = _context.RolesCategory.FirstOrDefault(r => r.Description.ToLower() == StaticRoleNames.Tenants.Insurer.ToLower());
            if (InsurerRoleDesc == null)
            {
                InsurerRoleDesc = _context.RolesCategory.Add(
                      new RolesCategories.RolesCategory
                      {
                          Description = StaticRoleNames.Tenants.Insurer,
                          Enabled = true
                      });

                _context.SaveChanges();
            }

            var BrokerRoleDesc = _context.RolesCategory.FirstOrDefault(r => r.Description.ToLower() == StaticRoleNames.Tenants.Broker.ToLower());
            if (BrokerRoleDesc == null)
            {
                BrokerRoleDesc = _context.RolesCategory.Add(
                      new RolesCategories.RolesCategory
                      {
                          Description = StaticRoleNames.Tenants.Broker,
                          Enabled = true
                      });

                _context.SaveChanges();
            }

            var NoneRoleDesc = _context.RolesCategory.FirstOrDefault(r => r.Description.ToLower() == StaticRoleNames.Tenants.None.ToLower());
            if (NoneRoleDesc == null)
            {
                NoneRoleDesc = _context.RolesCategory.Add(
                      new RolesCategories.RolesCategory
                      {
                          Description = StaticRoleNames.Tenants.None,
                          Enabled = true
                      });

                _context.SaveChanges();
            }
        }

        private void CreateHostRoleAndUsers()
        {
            //Admin role for host

            var adminRoleForHost = _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Admin);
            if (adminRoleForHost == null)
            {
                adminRoleForHost = _context.Roles.Add(new Role(null, StaticRoleNames.Host.Admin, StaticRoleNames.Host.Admin) { IsStatic = true, IsDefault = true, RoleCategoryID = 1 });
                _context.SaveChanges();
            }

            //admin user for host

            var adminUserForHost = _context.Users.FirstOrDefault(u => u.TenantId == null && u.UserName == User.AdminUserName);
            if (adminUserForHost == null)
            {
                adminUserForHost = _context.Users.Add(
                    new User
                    {
                        TenantId = null,
                        UserName = User.AdminUserName,
                        Name = "admin",
                        Surname = "admin",
                        EmailAddress = "admin@aspnetzero.com",
                        IsEmailConfirmed = true,
                        ShouldChangePasswordOnNextLogin = true,
                        IsActive = true,
                        Password = "AM4OLBpptxBYmM79lGOX9egzZk3vIQU3d/gFCJzaBjAPXzYIK3tQ2N7X4fcrHtElTw==" //123qwe
                    });
                _context.SaveChanges();
                
                //Assign Admin role to admin user
                _context.UserRoles.Add(new UserRole(null, adminUserForHost.Id, adminRoleForHost.Id));
                _context.SaveChanges();

                //Grant all permissions
                var permissions = PermissionFinder
                    .GetAllPermissions(new AppAuthorizationProvider(true))
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host))
                    .ToList();

                foreach (var permission in permissions)
                {
                    _context.Permissions.Add(
                        new RolePermissionSetting
                        {
                            TenantId = null,
                            Name = permission.Name,
                            IsGranted = true,
                            RoleId = adminRoleForHost.Id
                        });
                }

                _context.SaveChanges();

                //User account of admin user
                _context.UserAccounts.Add(new UserAccount
                {
                    TenantId = null,
                    UserId = adminUserForHost.Id,
                    UserName = User.AdminUserName,
                    EmailAddress = adminUserForHost.EmailAddress
                });

                _context.SaveChanges();
                
            }
        }
    }
}