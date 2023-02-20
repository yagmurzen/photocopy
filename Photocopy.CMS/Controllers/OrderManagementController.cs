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
        public IActionResult Index(int orderId)
        {
            return View(_service.GetOrderDetail(orderId));
        }
        [HttpPost]
        public IActionResult Index(OrderListDto order)
        {
            _service.SetOrderDetail(order);
            return RedirectToAction("Index", "OrderManagement", new  { orderId=order.Id });
        }

        public IActionResult OrderManagementList()
        {
            return View(_service.GetOrders());
        }
        
        public async Task<IActionResult> UploadData(Guid uploadDataId)
        {
            UploadDataDto data = _service.GetUploadData(uploadDataId);
            var bytes = Convert.FromBase64String(data.FileData);
            return File(bytes, "application/pdf", data.FileName);
        }

        public IActionResult PriceList()
        {
           IList<LookupPriceListDto> list= _service.GetAllLookupPrice();
            return View("/Views/OrderManagement/LookupPrice.cshtml", list);

        }

        [HttpPost]
        public IActionResult SetLookupPrice(SetLookupPriceCmsDto lookupPriceCmsDto)
        {
            _service.SetLookupPrice(lookupPriceCmsDto);
            return RedirectToAction("PriceList", "OrderManagement");
        }

    }
}
