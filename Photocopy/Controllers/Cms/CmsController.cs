using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Photocopy.Entities.Domain;
using System.Diagnostics;
using System.Security.Claims;
using Photocopy.Core.AppContext;
using Microsoft.AspNetCore.Authorization;

namespace Photocopy.CMS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("cms/anasayfa")]
        public IActionResult Index()
        {
            return View("/Views/_CMS/Home/Index.cshtml");
        }
    }
}