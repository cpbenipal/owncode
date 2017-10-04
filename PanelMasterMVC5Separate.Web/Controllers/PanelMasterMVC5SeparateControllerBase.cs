using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Microsoft.AspNet.Identity;

namespace PanelMasterMVC5Separate.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// Add your methods to this class common for all controllers.
    /// </summary>
    public abstract class PanelMasterMVC5SeparateControllerBase : AbpController
    {
        protected PanelMasterMVC5SeparateControllerBase()
        {
            LocalizationSourceName = PanelMasterMVC5SeparateConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}