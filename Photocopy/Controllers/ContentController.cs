using Azure.Core;
using IPara.DeveloperPortal.Core;
using IPara.DeveloperPortal.Core.Entity;
using IPara.DeveloperPortal.Core.Request;
using IPara.DeveloperPortal.Core.Response;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NuGet.Configuration;
using Photocopy.Core.Interface.Helper;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities.Domain;
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
            IPara.DeveloperPortal.Core.Settings settings = new()
            {
                PublicKey = _configuration.Ipara.PublicKey,//"8AWVLX6N3WJ0EYP", //"Public Magaza Anahtarı - size mağaza başvurunuz sonucunda gönderilen publik key (açık anahtar) bilgisini kullanınız.",
                PrivateKey = _configuration.Ipara.PrivateKey.ToString(),// "4NFUOHFTXFAHRANIE0W0YK601", //"Private Magaza Anahtarı  - size mağaza başvurunuz sonucunda gönderilen privaye key (gizli anahtar) bilgisini kullanınız.",
                BaseUrl = _configuration.Ipara.BaseUrl,// "https://api.ipara.com/", //iPara web servisleri API url'lerinin başlangıç bilgisidir. Restful web servis isteklerini takip eden kodlar halinde bulacaksınız. Örneğin "https://api.ipara.com/" + "/rest/payment/auth"  = "https://api.ipara.com/rest/payment/auth"
                Version = "1.0", // Kullandığınız iPara API versiyonudur.
                Mode = _configuration.Ipara.Mode,// "T", // Test -> T, entegrasyon testlerinin sırasında "T" modunu, canlı sisteme entegre olarak ödeme almaya başlamak için ise Prod -> "P" modunu kullanınız.
                HashString = string.Empty // Kullanacağınız hash bilgisini, bağlanmak istediğiniz web servis bilgisine göre doldurulmalıdır. Bu bilgileri Entegrasyon rehberinin ilgili web servise ait bölümde bulabilirsiniz.
            };

            OrderInfoDto orderInfoDto = _service.GetOrderInfo(id);
            

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
            request.SuccessUrl = _configuration.Ipara.WebsiteUrl+"/ThreeDResultSuccess?id=" +id;
            request.FailUrl = _configuration.Ipara.WebsiteUrl + "/Content/ThreeDResultFail";



            #region Sipariş veren bilgileri
            request.Purchaser = new();
            request.Purchaser.Name = orderInfoDto.Customer.Name;
            request.Purchaser.SurName = orderInfoDto.Customer.LastName;
            request.Purchaser.Email = orderInfoDto.Customer.Email;
            request.Purchaser.GsmPhone = orderInfoDto.Customer.PhoneNumber;
            //request.Purchaser.IdentityNumber = "12345678901";
            request.Purchaser.ClientIp = "127.0.0.1";
            #endregion

            #region Fatura bilgileri

            request.Purchaser.InvoiceAddress = new PurchaserAddress();
            request.Purchaser.InvoiceAddress.Name = orderInfoDto.Customer.Name;
            request.Purchaser.InvoiceAddress.SurName = orderInfoDto.Customer.LastName;
            request.Purchaser.InvoiceAddress.Address = orderInfoDto.Customer.Adrress;
            //request.Purchaser.InvoiceAddress.ZipCode = "34782";
            request.Purchaser.InvoiceAddress.CityCode = orderInfoDto.Customer.City;
            //request.Purchaser.InvoiceAddress.IdentityNumber = "1234567890";
            request.Purchaser.InvoiceAddress.CountryCode = "TR";
            //request.Purchaser.InvoiceAddress.TaxNumber = "123456";
            //request.Purchaser.InvoiceAddress.TaxOffice = "Kozyatağı";
            request.Purchaser.InvoiceAddress.CompanyName = "iPara";
            request.Purchaser.InvoiceAddress.PhoneNumber = orderInfoDto.Customer.PhoneNumber;

            #endregion

            string threeDform = ThreeDPaymentRequest.Execute(request, settings);
            HttpResponseWritingExtensions.WriteAsync(this.Response, threeDform);
            
            return View();
        }

        public ActionResult ThreeDResultSuccess(int id)
        {
            IPara.DeveloperPortal.Core.Settings settings = new()
            {
                PublicKey = _configuration.Ipara.PublicKey,//"8AWVLX6N3WJ0EYP", //"Public Magaza Anahtarı - size mağaza başvurunuz sonucunda gönderilen publik key (açık anahtar) bilgisini kullanınız.",

                PrivateKey = _configuration.Ipara.PrivateKey.ToString(),// "4NFUOHFTXFAHRANIE0W0YK601", //"Private Magaza Anahtarı  - size mağaza başvurunuz sonucunda gönderilen privaye key (gizli anahtar) bilgisini kullanınız.",

                BaseUrl = _configuration.Ipara.BaseUrl,// "https://api.ipara.com/", //iPara web servisleri API url'lerinin başlangıç bilgisidir. Restful web servis isteklerini takip eden kodlar halinde bulacaksınız. Örneğin "https://api.ipara.com/" + "/rest/payment/auth"  = "https://api.ipara.com/rest/payment/auth"

                Version = "1.0", // Kullandığınız iPara API versiyonudur.

                Mode = _configuration.Ipara.Mode,// "T", // Test -> T, entegrasyon testlerinin sırasında "T" modunu, canlı sisteme entegre olarak ödeme almaya başlamak için ise Prod -> "P" modunu kullanınız.

                HashString = string.Empty // Kullanacağınız hash bilgisini, bağlanmak istediğiniz web servis bilgisine göre doldurulmalıdır. Bu bilgileri Entegrasyon rehberinin ilgili web servise ait bölümde bulabilirsiniz.
            };
            #region MyRegion
            ThreeDPaymentResponse paymentResponse = new ThreeDPaymentResponse();
            paymentResponse.OrderId = Request.Form["orderId"];
            paymentResponse.Result = Request.Form["result"];
            paymentResponse.Amount = Request.Form["amount"];
            paymentResponse.Mode = Request.Form["mode"];
            if (Request.Form["errorCode"].ToString() != null)
                paymentResponse.ErrorCode = Request.Form["errorCode"];

            if (Request.Form["errorMessage"].ToString() != null)
                paymentResponse.ErrorMessage = Request.Form["errorMessage"];

            if (Request.Form["transactionDate"].ToString() != null)
                paymentResponse.TransactionDate = Request.Form["transactionDate"];

            if (Request.Form["hash"].ToString() != null)
                paymentResponse.Hash = Request.Form["hash"];

            #region info
            //var request = new ThreeDPaymentRequest();

            //#region Request New
            //request.OrderId = Request.Form["orderId"];
            //request.Echo = "Echo";
            //request.Mode = settings.Mode;
            //request.Amount = "10000"; // 100 tL
            //request.CardOwnerName = "Fatih Coşkun";
            //request.CardNumber = "4282209027132016";
            //request.CardExpireMonth = "05";
            //request.CardExpireYear = "18";
            //request.Installment = "1";
            //request.Cvc = "000";
            ////request.ThreeD = "true";
            ////request.ThreeDSecureCode = Request.Form["threeDSecureCode"];
            //#endregion

            //#region Sipariş veren bilgileri
            //request.Purchaser = new Purchaser();
            //request.Purchaser.BirthDate = "1986-07-11";
            //request.Purchaser.GsmPhone = "5881231212";
            //request.Purchaser.IdentityNumber = "1234567890";

            //#endregion

            //#region Fatura bilgileri

            //request.Purchaser.InvoiceAddress = new PurchaserAddress();
            //request.Purchaser.InvoiceAddress.Name = "Murat";
            //request.Purchaser.InvoiceAddress.SurName = "Kaya";
            //request.Purchaser.InvoiceAddress.Address = "Mevlüt Pehlivan Mah. Multinet Plaza Şişli";
            //request.Purchaser.InvoiceAddress.ZipCode = "34782";
            //request.Purchaser.InvoiceAddress.CityCode = "34";
            //request.Purchaser.InvoiceAddress.IdentityNumber = "1234567890";
            //request.Purchaser.InvoiceAddress.CountryCode = "TR";
            //request.Purchaser.InvoiceAddress.TaxNumber = "123456";
            //request.Purchaser.InvoiceAddress.TaxOffice = "Kozyatağı";
            //request.Purchaser.InvoiceAddress.CompanyName = "iPara";
            //request.Purchaser.InvoiceAddress.PhoneNumber = "2122222222";

            //#endregion

            //#region Kargo Adresi bilgileri

            //request.Purchaser.ShippingAddress = new PurchaserAddress();
            //request.Purchaser.ShippingAddress.Name = "Murat";
            //request.Purchaser.ShippingAddress.SurName = "Kaya";
            //request.Purchaser.ShippingAddress.Address = "Mevlüt Pehlivan Mah. Multinet Plaza Şişli";
            //request.Purchaser.ShippingAddress.ZipCode = "34782";
            //request.Purchaser.ShippingAddress.CityCode = "34";
            //request.Purchaser.ShippingAddress.IdentityNumber = "1234567890";
            //request.Purchaser.ShippingAddress.CountryCode = "TR";
            //request.Purchaser.ShippingAddress.PhoneNumber = "2122222222";

            //#endregion

            //#region Ürün bilgileri

            //request.Products = new List<Product>();
            //Product p = new Product();
            //p.Title = "Telefon";
            //p.Code = "TLF0001";
            //p.Price = "5000";
            //p.Quantity = 1;
            //request.Products.Add(p);
            //p = new Product();
            //p.Title = "Bilgisayar";
            //p.Code = "BLG0001";
            //p.Price = "5000";
            //p.Quantity = 1;
            //request.Products.Add(p);

            //#endregion


            //var response = ThreeDPaymentRequest.Execute(request, settings);  
            #endregion

            #endregion

            #region Paymnet Done Response

            OrderListDto order = new OrderListDto();

            order.Id = id;
            order.PaymentState = (int)PaymentState.Done;
            order.TransactionDate = DateTime.Now;
            order.IparaOrderId = paymentResponse.OrderId;

            _service.SetOrderDetail(order);
            //Ödeme Logu bas
            #endregion
            return View(paymentResponse);
        }
        public ActionResult ThreeDResultFail()
        {
            ThreeDPaymentResponse response = new ThreeDPaymentResponse();
            response.OrderId = Request.Form["orderId"];
            response.Result = Request.Form["result"];
            response.Amount = Request.Form["amount"];
            response.Mode = Request.Form["mode"];
            if (Request.Form["errorCode"].ToString() != null)
                response.ErrorCode = Request.Form["errorCode"];

            if (Request.Form["errorMessage"].ToString() != null)
                response.ErrorMessage = Request.Form["errorMessage"];

            if (Request.Form["transactionDate"].ToString() != null)
                response.TransactionDate = Request.Form["transactionDate"];

            if (Request.Form["hash"].ToString() != null)
                response.Hash = Request.Form["hash"];
            return View(response);
        }
        public ActionResult BinInqury()
        {
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