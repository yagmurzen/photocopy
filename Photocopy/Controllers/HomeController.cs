using Azure.Core;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Photocopy.Core.Interface.Helper;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities.Dto.WebUI;
using Photocopy.Helper;
using Photocopy.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Photocopy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientExtensions _ext;
        private IContentService _service;

        public HomeController(ILogger<HomeController> logger, IHttpClientExtensions ext, IContentService service)
        {
            _logger = logger;
            _ext = ext;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();           
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Slider()
        {
            return View(new HomeContentDto
            {
                SliderList = _service.GetSliderlist()
            });
        }

    }
}