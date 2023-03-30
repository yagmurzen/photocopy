using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Photocopy.Entities.Domain;
using System.Security.Claims;
using Photocopy.Core.AppContext;
using Photocopy.Entities.Dto;
using Photocopy.Entities;
using Photocopy.Core.Interface.Services;

namespace Photocopy.CMS.Controllers
{
    [Route("cms")]
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;

        public AuthController(ILogger<AuthController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        [Route("panel")]
        public IActionResult Index(ErrorResult res)
        {
            return View("/Views/_CMS/Auth/Index.cshtml",res);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string username, string password)
        {

            UserDto user = _userService.GetUserByUsername(username,password);

            if (user == null)            
                return RedirectToAction("panel", "cms",new ErrorResult { Message = "Kullanıcı Bilgileri Hatalıdır." });
            
            var result = user.Password == password;
            ClaimsIdentity identity = null;
            bool isAuthenticate = false;
            if (result)
            {
                identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                foreach (var item in user.UserRoles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, item.Name));
                }
                identity.AddClaim(new Claim(ClaimTypes.Name, username));
                isAuthenticate = true;
            }
            if (isAuthenticate)
            {
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("anasayfa","cms");
            }
            return RedirectToAction("panel", "cms", new ErrorResult { Message = "Kullanıcı Bilgileri Hatalıdır." });

        }
        [Route("cikis")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("panel", "cms");
        }
    }
}
