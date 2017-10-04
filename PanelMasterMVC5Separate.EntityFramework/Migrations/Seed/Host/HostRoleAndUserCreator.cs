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

            //CreateDefaultStoredproc();
        }

        /*private void CreateDefaultStoredproc()
        {
            var sp_JobDetails = "CREATE PROCEDURE [dbo].sp_JobDetails " +    
                "AS " +
                "BEGIN " +    
                    "SET NOCOUNT ON; " +

                    "SELECT dbo.brClient.Id, dbo.brClient.Name, dbo.brClient.Surname, dbo.brClient.Title, dbo.brClient.Email, " +
                    "dbo.brClient.Tel, dbo.brClient.CommunicationType, dbo.brClient.ContactAfterService, dbo.brJobs.ClientID, " + 
                    "dbo.brJobs.ManufactureID, dbo.brJobs.ModelID, dbo.brJobs.InsuranceID, dbo.brJobs.BrokerID, dbo.brJobs.BranchID, " +
                    "dbo.brJobs.FinancialID, dbo.brJobs.CSAID, dbo.brJobs.EstimatorID, dbo.brJobs.ProductiveStaffID, " + 
                    "dbo.brJobs.ClaimStatusID, dbo.brJobs.ClaimEventID, dbo.brJobs.RegNo, dbo.brJobs.VinNumber, dbo.brJobs.Colour, " +
                    "dbo.brJobs.Year, dbo.brJobs.UnderWaranty, dbo.brJobs.New_Comeback " + 
                    "FROM dbo.brClient INNER JOIN dbo.brJobs ON dbo.brClient.Id = dbo.brJobs.ClientID " +

                "END ";

            _context.Database.ExecuteSqlCommand(sp_JobDetails);
        }*/

        private void CreateHostRoleAndUsers()
        {
            //Admin role for host

            var adminRoleForHost = _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Admin);
            if (adminRoleForHost == null)
            {
                adminRoleForHost = _context.Roles.Add(new Role(null, StaticRoleNames.Host.Admin, StaticRoleNames.Host.Admin) { IsStatic = true, IsDefault = true });
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