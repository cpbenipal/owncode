using Abp.Dependency;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Views;

namespace PanelMasterMVC5Separate.Web.Views
{
    public abstract class PanelMasterMVC5SeparateWebViewPageBase : PanelMasterMVC5SeparateWebViewPageBase<dynamic>
    {
       
    }

    public abstract class PanelMasterMVC5SeparateWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        public IAbpSession AbpSession { get; private set; }
        
        protected PanelMasterMVC5SeparateWebViewPageBase()
        {
            AbpSession = IocManager.Instance.Resolve<IAbpSession>();
            LocalizationSourceName = PanelMasterMVC5SeparateConsts.LocalizationSourceName;
        }
    }
}