using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Microsoft.AspNet.Identity;
using PanelMasterMVC5Separate.Authorization.Claim;
using PanelMasterMVC5Separate.MultiTenancy;

namespace PanelMasterMVC5Separate
{
    /// <summary>
    /// All application services in this application is derived from this class.
    /// We can add common application service methods here.
    /// </summary>
    public abstract class PanelMasterMVC5SeparateAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        public SignonPlans SignonPlans { get; set; }

        protected PanelMasterMVC5SeparateAppServiceBase()
        {
            LocalizationSourceName = PanelMasterMVC5SeparateConsts.LocalizationSourceName;
        }

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual User GetCurrentUser()
        {
            var user = UserManager.FindById(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            using (CurrentUnitOfWork.SetTenantId(null))
            {
                return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
            }
        }

        protected virtual Tenant GetCurrentTenant()
        {
            using (CurrentUnitOfWork.SetTenantId(null))
            {
                return TenantManager.GetById(AbpSession.GetTenantId());
            }
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}