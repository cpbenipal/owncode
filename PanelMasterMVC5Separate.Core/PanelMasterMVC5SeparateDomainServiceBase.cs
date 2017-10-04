using Abp.Domain.Services;

namespace PanelMasterMVC5Separate
{
    public abstract class PanelMasterMVC5SeparateDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected PanelMasterMVC5SeparateDomainServiceBase()
        {
            LocalizationSourceName = PanelMasterMVC5SeparateConsts.LocalizationSourceName;
        }
    }
}
