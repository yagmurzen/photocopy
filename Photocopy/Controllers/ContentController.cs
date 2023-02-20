using Azure.Core;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Photocopy.Core.Interface.Helper;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities.Dto.WebUI;
using Photocopy.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Photocopy.Controllers
{
    public class ContentController : Controller
    {
        private readonly ILogger<ContentController> _logger;
        private IOrderService _service;
        ICryptoHelper _cryptoHelper;
        public ContentController(ILogger<ContentController> logger, IOrderService service, ICryptoHelper cryptoHelper)
        {
            _logger = logger;
            _service = service;
            _cryptoHelper = cryptoHelper;
        }

        [Route("iletisim")]
        public IActionResult Contact()
        {
            return View();
        }

		[Route("sepet")]
		public IActionResult OrderBox()
		{
			return View();
		}
		[Route("siparis-detay")]
        [HttpPost]
		public IActionResult OrderDetail(int siparisId)
		{
            OrderInfoDto orderInfoDto = _service.GetOrderInfo(siparisId);

            return View("/Views/Content/OrderDetail.cshtml", orderInfoDto);
        }


        [Route("fotokopi-hesaplama")]
		public IActionResult Calculate()
		{
			return View();
		}

        [HttpPost]
        public IActionResult SendEmail()
        {
            return Redirect("iletisim");
        }

        [HttpPost, HttpGet]
        [Route("İlceler")]
        public IActionResult GetMyViewComponent(string cityId)
        {
            return ViewComponent("District", new { cityId = cityId });
        }

    }
}