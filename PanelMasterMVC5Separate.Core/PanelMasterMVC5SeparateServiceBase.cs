using Abp;

namespace PanelMasterMVC5Separate
{
    /// <summary>
    /// This class can be used as a base class for services in this application.
    /// It has some useful objects property-injected and has some basic methods most of services may need to.
    /// It's suitable for non domain nor application service classes.
    /// For domain services inherit <see cref="PanelMasterMVC5SeparateDomainServiceBase"/>.
    /// For application services inherit PanelMasterMVC5SeparateAppServiceBase.
    /// </summary>
    public abstract class PanelMasterMVC5SeparateServiceBase : AbpServiceBase
    {
        protected PanelMasterMVC5SeparateServiceBase()
        {
            LocalizationSourceName = PanelMasterMVC5SeparateConsts.LocalizationSourceName;
        }
    }
}