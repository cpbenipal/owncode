using System.Web.Mvc;
using Abp.Auditing;
using Abp.Web.Mvc.Authorization;

namespace PanelMasterMVC5Separate.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ApplicationController : PanelMasterMVC5SeparateControllerBase
    {
        [DisableAuditing]
        public ActionResult Index()
        {
            /* Enable next line to redirect to Multi-Page Application */
        //     return RedirectToAction("Me", "Home"); 

             return View("~/App/common/views/layout/layout.cshtml"); //Layout of the angular application.
        }
    }
}