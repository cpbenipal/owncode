using System.Web.Mvc;

namespace PanelMasterMVC5Separate.Web.Controllers
{
    public class AboutController : PanelMasterMVC5SeparateControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}