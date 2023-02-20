using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using NuGet.ContentModel;
using Photocopy.Core.Interface.Helper;
using Photocopy.Core.Interface.Repository;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto.WebUI;
using Photocopy.Entities.Model;
using Photocopy.Helper;
using Photocopy.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Photocopy.Controllers
{
    public class BasketController : Controller
    {
        private readonly ILogger<BasketController> _logger;
        ICookieHelper _cookieHelper;
        private IOrderService _service;
        private const string orderCookieName= "bskt";
        private readonly IHttpClientExtensions _httpClientExtensions;
        private IUnitOfWork _unitOfWork;


        public BasketController(ILogger<BasketController> logger, ICryptoHelper cryptoHelper, IOrderService service, ICookieHelper cookieHelper, IHttpClientExtensions httpClientExtensions,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _service = service;
            _cookieHelper = cookieHelper;
            _httpClientExtensions= httpClientExtensions; ;
            _unitOfWork = unitOfWork;
        }

        //Sepet Açılınca Şifrelenmiş Cookie kontrol edilir. Boş değilse Şifre çözülerek Data ekrana getirilir.
        public IActionResult Index()
        {
            var cookie = _cookieHelper.GetCookie(orderCookieName);

            if (cookie == null) return View("/Views/Basket/Index.cshtml");

            return View("/Views/Basket/Index.cshtml", JsonHelper.Deserialize<IList<CalculateDto>>(cookie));
        }

        //Ürün Ekleme --> Dosya Yükleme ile başlar. Dosya Database Base64 formatında yüklenerek Dönüş id değeri sepet içerisinde kullanılır.
        [Route("DosyaYukle")]
        [HttpPost]
        public UploadDataDto UploadData(IFormFile File)
        {
            Guid id =_service.UploadDataAsync(new UploadDataDto { FileData = ConverterHelper.Base64ToImage(File), FileName = File.FileName }).Result;
            return new UploadDataDto { Id = id, FileName = File.FileName };
        }
               
        //Eklenen Dosya ile birlikte Özellikler Girilerek otomatik hesaplama yapılır. Buradaki Hesaplama Parametreleri db'de tutulmaktadır.
        [HttpPost]
        [Route("Hesapla")]
        public CalculateDto Calculate(CalculateDto inModel)
        {
            CalculateDto price = _service.Calculate(inModel);
            return price;
        }

        [HttpPost,HttpGet]
        [Route("SepeteEkle")]
        public IActionResult GetMyViewComponent(CalculateDto inModel)
        {
            return ViewComponent("OrderBoxItem", new { inModel = inModel });
        }

        [HttpPost, HttpGet]
        [Route("SepettenCikar")]
        public async Task<IActionResult> RemoveOrderAsync(Guid id)
        {
            var outModel = String.Empty;

            var cookie = _cookieHelper.GetCookie(orderCookieName);

            if (cookie != "")
            {
                int basketId = Convert.ToInt32(cookie);
                //Expire olmuş bir sepet ise yeniden oluşturulur.
                Basket? basket = _unitOfWork.Baskets.GetByIdAsync(x => x.IsDeleted == false && x.TransactionDate > DateTime.Now.AddDays(-1) && x.Id == basketId);
               
                if (basket != null)
                {
                    IList<BasketDetail> basketDetails = _unitOfWork.BasketDetails.GetAllBasketDetails(x => x.IsDeleted == false && x.BasketId == basket.Id).ToList();

                    BasketDetail detail = basketDetails.Where(x => x.UploadDataId == id).Single();
                    detail.IsDeleted = true;
                    detail = await _unitOfWork.BasketDetails.Update(detail);
                    await _unitOfWork.CommitAsync();
                }
            }

            return ViewComponent("OrderBoxItem");

        }

        //Sipariş Oluşturma Fonksiyonu
        [HttpPost]
        [Route("Siparis")]
        public IActionResult CreateOrder(CreatedOrderDto model)
        {
            var cookie = _cookieHelper.GetCookie(orderCookieName);

            if (cookie == "") return View("/Views/Content/OrderBox.cshtml");

            int basketId = Convert.ToInt32(cookie);

            IList<BasketDetail> basketDetails = _unitOfWork.BasketDetails.GetAllBasketDetails(x => x.IsDeleted == false && x.BasketId == basketId).ToList();
            IList<CalculateDto> list = new List<CalculateDto>(); // JsonHelper.Deserialize<IList<CalculateDto>>(cookie);

            foreach (var item in basketDetails)
            {
                list.Add(new CalculateDto
                {
                    Binding = item.Binding,
                    Colourfull = item.Colourfull,
                    Combination = item.Combination,
                    Count = item.Count,
                    FileName = item.UploadData.FileName,
                    FilePrice = item.FilePrice,
                    Format = item.Format,
                    PagePrice = item.PagePrice,
                    PaperType = item.PaperType,
                    PdfPageCount = item.PdfPageCount,
                    Quality = item.Quality,
                    Rotate = item.Rotate,
                    Side = item.Side,
                    UnitPrice = item.UnitPrice,
                    UploadDataId = item.UploadDataId
                });

            }
            model.OrderDetails = list;

            CustomerDto customer=_service.CreateCustomerAsync(model.Customer).GetAwaiter().GetResult();
            model.Customer.Address.CustomerId= customer.Id.Value;
            _service.CreateCustomerAddressAsync(model.Customer.Address).GetAwaiter().GetResult();

            model.Customer.Id = customer.Id.Value;
            CreatedOrderDto createdOrderDto=_service.CreateOrderAsync(model).GetAwaiter().GetResult();

            model.Id= createdOrderDto.Id;
            _service.CreateOrderDetailAsync(model).GetAwaiter().GetResult();

            OrderInfoDto orderInfoDto = _service.GetOrderInfo(createdOrderDto.Id.Value);
            
            MngOrder orderModel = new MngOrder();
            orderModel.order = new Entities.Model.Order
            {
                referenceId = "SIPARIS" + createdOrderDto.Id.ToString(), //*
                barcode = "SIPARIS" + createdOrderDto.Id.ToString(),//*
                billOfLandingId = "İrsaliye 1",
                isCOD = 0,
                codAmount = 0,
                shipmentServiceType = 1,
                packagingType = 1,
                content = "İçerik 1",
                smsPreference1 = 1,
                smsPreference2 = 0,
                smsPreference3 = 0,
                paymentType = 1,
                deliveryType = 1,
                description = "Açıklama 1",
                marketPlaceShortCode = "",
                marketPlaceSaleCode = "",
                pudoId = ""
            };
            orderModel.recipient = new Recipient
            {
                customerId =null, //??//*
                refCustomerId = "",
                cityCode = Convert.ToInt32( model.Customer.Address.CityCode),//*
                cityName = "",
                districtName = "",
                districtCode = Convert.ToInt32(model.Customer.Address.DistrictCode),//*
                address = model.Customer.Address.Address,//*
                bussinessPhoneNumber ="",
                email = model.Customer.Email,//*
                taxOffice = "",
                taxNumber = "",
                fullName = model.Customer.Name + " " +model.Customer.LastName,//*
                homePhoneNumber = "",
                mobilePhoneNumber = model.Customer.PhoneNumber//*
            };
            orderModel.orderPieceList = new List<Orderpiecelist>();
            orderModel.orderPieceList.Add(new Orderpiecelist
            {
                barcode = "SIPARIS"+ createdOrderDto.Id.ToString() + "_PARCA1",//*
                desi = 0,
                kg = 0,
                content = "Parça açıklama 1"

            });

           
            var apiResponse = _httpClientExtensions.CreateOrder(orderModel);
            //[{"orderInvoiceId":"720322","orderInvoiceDetailId":"720856","shipperBranchCode":"03401700"}]
            //Dönüş Değeri Update
            if (apiResponse.Success)
            {
                var responseModel = apiResponse.response.Repsonse;
                _cookieHelper.RemoveCookie(orderCookieName);
                return View("/Views/Content/OrderDetail.cshtml", orderInfoDto);
            }
            else
            {
               _service.RemoveOrder(model.Id.Value, false).GetAwaiter().GetResult();
                return View("/Views/Content/OrderBox.cshtml");
            }
        }

        #region kullanılmıyor

        //[HttpPost]
        //[Route("SiparisDurumu")]
        //public string GetOrderStatus(string siparisNo, string telefon)
        //{
        //    string status = _service.GetOrderStatus(siparisNo, telefon);
        //    return status;
        //} 
        #endregion
    }
}