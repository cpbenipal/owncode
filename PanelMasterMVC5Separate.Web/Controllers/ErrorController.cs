using System.Web.Mvc;
using Abp.Auditing;

namespace PanelMasterMVC5Separate.Web.Controllers
{
    public class ErrorController : PanelMasterMVC5SeparateControllerBase
    {
        [DisableAuditing]
        public ActionResult E404()
        {
            return View();
        }
    }
}