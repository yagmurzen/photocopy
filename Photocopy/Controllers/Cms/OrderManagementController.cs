using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities.Dto;

namespace Photocopy.CMS.Controllers
{
    public class OrderManagementController : Controller
    {
        private readonly ILogger<OrderManagementController> _logger;
        private IOrderService _service;
        private readonly IMapper _mapper;

        public OrderManagementController(ILogger<OrderManagementController> logger, IOrderService service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("cms/siparis-detay")]
        public IActionResult Index(int orderId)
        {
            return View("/Views/_CMS/OrderManagement/Index.cshtml", _service.GetOrderDetail(orderId));
        }
        [HttpPost]
        [Route("cms/siparis-detay")]
        public IActionResult Index(OrderListDto order)
        {
            _service.SetOrderDetail(order);
            return RedirectToAction("siparis-detay", "cms", new  { orderId=order.Id });
        }
        [Route("cms/siparis-yonetimi")]
        public IActionResult OrderManagementList()
        {
            return View("/Views/_CMS/OrderManagement/OrderManagementList.cshtml", _service.GetOrders());
        }
        [Route("cms/UploadData")]
        public async Task<IActionResult> UploadData(Guid uploadDataId)
        {
            UploadDataDto data = _service.GetUploadData(uploadDataId);
            var bytes = Convert.FromBase64String(data.FileData);
            return File(bytes, "application/pdf", data.FileName);
        }
        [Route("cms/fiyat-listesi")]
        public IActionResult PriceList()
        {
           IList<LookupPriceListDto> list= _service.GetAllLookupPrice();
            return View("/Views/_CMS/OrderManagement/LookupPrice.cshtml", list);

        }

        [HttpPost]
        [Route("cms/SetLookupPrice")]
        public IActionResult SetLookupPrice(SetLookupPriceCmsDto lookupPriceCmsDto)
        {
            _service.SetLookupPrice(lookupPriceCmsDto);
            return RedirectToAction("fiyat-listesi","cms", "OrderManagement");
        }

    }
}
