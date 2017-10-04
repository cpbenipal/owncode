using Abp.WebApi.Controllers;

namespace PanelMasterMVC5Separate.WebApi
{
    public abstract class PanelMasterMVC5SeparateApiControllerBase : AbpApiController
    {
        protected PanelMasterMVC5SeparateApiControllerBase()
        {
            LocalizationSourceName = PanelMasterMVC5SeparateConsts.LocalizationSourceName;
        }
    }
}