using Microsoft.AspNetCore.Mvc;

namespace Photocopy.CMS.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MenuList()
        {
            return View();
        }
    }
}
