using Azure.Core;
using IPara.DeveloperPortal.Core;
using IPara.DeveloperPortal.Core.Entity;
using IPara.DeveloperPortal.Core.Request;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Photocopy.Core.Interface.Helper;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities.Dto;
using Photocopy.Entities.Dto.WebUI;
using Photocopy.Entities.Model;
using Photocopy.Helper;
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
        private IContentService _contentService;
        ICryptoHelper _cryptoHelper;
        IEmailHelper _emailHelper;
        private  readonly PaymentModel _configuration;


        public ContentController(ILogger<ContentController> logger, IOrderService service, ICryptoHelper cryptoHelper, IContentService contentService, IEmailHelper emailHelper, IOptions<PaymentModel> configuration)
        {
            _logger = logger;
            _service = service;
            _cryptoHelper = cryptoHelper;
            _contentService = contentService;
            _emailHelper = emailHelper;
            _configuration = configuration.Value;

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

        [Route("odeme-sayfasi")]
        public IActionResult PaymentPage(decimal price,int id)
        {
            return View("/Views/Payment/Index.cshtml",new OrderListDto {TotalPrice= price, Id=id });
        }

        [HttpPost]
        [Route("payment")]
        public ActionResult OrderDetail(int id, string nameSurname, string cardNumber, string cvc, string month, string year, string amount, string installment)
        {
            Settings settings = new()
            {
                PublicKey = _configuration.Ipara.PublicKey,//"8AWVLX6N3WJ0EYP", //"Public Magaza Anahtarı - size mağaza başvurunuz sonucunda gönderilen publik key (açık anahtar) bilgisini kullanınız.",

                PrivateKey = _configuration.Ipara.PrivateKey.ToString(),// "4NFUOHFTXFAHRANIE0W0YK601", //"Private Magaza Anahtarı  - size mağaza başvurunuz sonucunda gönderilen privaye key (gizli anahtar) bilgisini kullanınız.",

                BaseUrl = _configuration.Ipara.BaseUrl,// "https://api.ipara.com/", //iPara web servisleri API url'lerinin başlangıç bilgisidir. Restful web servis isteklerini takip eden kodlar halinde bulacaksınız. Örneğin "https://api.ipara.com/" + "/rest/payment/auth"  = "https://api.ipara.com/rest/payment/auth"

                Version = "1.0", // Kullandığınız iPara API versiyonudur.

                Mode = "T",// "T", // Test -> T, entegrasyon testlerinin sırasında "T" modunu, canlı sisteme entegre olarak ödeme almaya başlamak için ise Prod -> "P" modunu kullanınız.

                HashString = string.Empty // Kullanacağınız hash bilgisini, bağlanmak istediğiniz web servis bilgisine göre doldurulmalıdır. Bu bilgileri Entegrasyon rehberinin ilgili web servise ait bölümde bulabilirsiniz.
            };
            ThreeDPaymentRequest request = new();
            request.OrderId = Guid.NewGuid().ToString();
            request.Version = settings.Version;
            request.Amount = amount;
            request.CardOwnerName = nameSurname;
            request.CardNumber = cardNumber;
            request.CardExpireMonth = month;
            request.CardExpireYear = year;
            request.Installment = installment;
            request.Cvc = cvc;

            request.Echo = "";
            request.Mode = settings.Mode;
            request.UserId = "";
            request.CardId = "";
            request.Language = "tr-TR";
            request.SuccessUrl = "https://apitest.ipara.com/rest/payment/threed/test/result";
            request.FailUrl = "https://apitest.ipara.com/rest/payment/threed/test/result";

            request.Purchaser = new();
            request.Purchaser.Name = "Murat";
            request.Purchaser.SurName = "Kaya";
            request.Purchaser.BirthDate = "1986-07-11";
            request.Purchaser.Email = "murat@kaya.com";
            request.Purchaser.GsmPhone = "5881231212";
            request.Purchaser.IdentityNumber = "12345678901";
            request.Purchaser.ClientIp = "127.0.0.1";

            //request.Products = new List<Product>();
            //Product p = new();
            //p.Title = "Telefon";
            //p.Code = "TLF0001";
            //p.Price = "5000";
            //p.Quantity = 1;
            //request.Products.Add(p);

            //p = new();
            //p.Title = "Bilgisayar";
            //p.Code = "BLG0001";
            //p.Price = "5000";
            //p.Quantity = 1;
            //request.Products.Add(p);

            string threeDform = ThreeDPaymentRequest.Execute(request, settings);
            HttpResponseWritingExtensions.WriteAsync(this.Response, threeDform);
            #region Paymnet Done Response
            if (true)
            {
                OrderListDto order = new OrderListDto();

                order.Id = id;
                order.PaymentState = 1;
                order.TransactionDate = DateTime.Now;

                _service.SetOrderDetail(order);
            }
            #endregion
            return View();
        }

        [Route("fotokopi-hesaplama")]
		public IActionResult Calculate()
		{
			return View();
		}

        [Route("SendContactMail")]
        [HttpPost]
        public IActionResult SendContactMail(ContactDto model)
        {
            _contentService.AddContactAsync(model);

            _emailHelper.SendEmail(model);

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