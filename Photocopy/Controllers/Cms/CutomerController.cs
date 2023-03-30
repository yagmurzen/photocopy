using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Photocopy.Core.AppContext;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto;
using Photocopy.Service.Services;

namespace Photocopy.CMS.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private ICustomerService _service;

        private readonly IMapper _mapper;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [Route("cms/musteri-detay")]
        [HttpGet]
        public IActionResult Index(int customerId)
        {
            CustomerDto customer = _service.GetCustomerById(customerId);

            return View("/Views/_CMS/Customer/Index.cshtml", customer);

        }
        [Route("cms/musteri-listesi")]
        public IActionResult CustomerList()
        {
            IList<CustomerListDto> customers = _service.GetCustomerList();
            return View("/Views/_CMS/Customer/CustomerList.cshtml", customers);

        }

        [Route("cms/musteri-detay")]
        [HttpPost]
        public async Task<IActionResult> Index(CustomerDto customer)
        {
            CustomerDto customerDto = await _service.SaveOrUpdate(customer);

            return RedirectToAction("musteri-detay","cms", new { customerId = customer.Id });

        }
        [Route("cms/DeleteCustomer")]
        public async Task<IActionResult> DeleteCustomerAsync(int customerId)
        {
            await _service.DeleteCustomer(customerId);
            return RedirectToAction("musteri-listesi", "cms");

        }
        [Route("cms/DeleteCustomerAddress")]
        public async Task<IActionResult> DeleteCustomerAddressAsync(int addressId,int customerId)
        {
            await _service.DeleteCustomerAddress(addressId);
            return RedirectToAction("musteri-detay","cms", new { customerId = customerId });

        }
    }
}
