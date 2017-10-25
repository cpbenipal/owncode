using System.Web.Mvc;
using Microsoft.Owin.Security;
using PanelMasterMVC5Separate.Web.Auth;


namespace PanelMasterMVC5Separate.Web.Controllers
{
    public class HomeController : PanelMasterMVC5SeparateControllerBase
    {
        private readonly IAuthenticationManager _authenticationManager;

        public HomeController(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        public ActionResult Index()
        {
            return View();
        }
        [Abp.Authorization.AbpAuthorize]
        public ActionResult Me()
        {
            return View();
        }

        public ActionResult Logout()
        {
            _authenticationManager.SignOutAll();
            return RedirectToAction("Index");
        }
    }
}