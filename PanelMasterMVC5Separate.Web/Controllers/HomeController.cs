using System.Web.Mvc;

namespace PanelMasterMVC5Separate.Web.Controllers
{
    public class HomeController : PanelMasterMVC5SeparateControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
        [Abp.Authorization.AbpAuthorize]
        public ActionResult Me()
        {
            return View();
        }
    }
}