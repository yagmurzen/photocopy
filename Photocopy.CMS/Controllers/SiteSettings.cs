using Microsoft.AspNetCore.Mvc;

namespace Photocopy.CMS.Controllers
{
    public class SiteSettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
