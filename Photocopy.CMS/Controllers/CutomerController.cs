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

        public IActionResult Index(int customerId)
        {
            CustomerDto customer = _service.GetCustomerById(customerId);

            return View(customer);

        }
        public IActionResult CustomerList()
        {
            IList<CustomerListDto> customers = _service.GetCustomerList();
            return View("/Views/Customer/CustomerList.cshtml", customers);

        }
        public async Task<IActionResult> CustomerAsync(CustomerDto customer)
        {
            CustomerDto customerDto = await _service.SaveOrUpdate(customer);

            return RedirectToAction("Index", new { customerId = customer.Id });

        }

        public IActionResult DeleteCustomer(int customerId)
        {
            _service.DeleteCustomer(customerId);
            return CustomerList();

        }
        public IActionResult DeleteCustomerAddress(int addressId,int customerId)
        {
            _service.DeleteCustomerAddress(addressId);
            return RedirectToAction("Index", new { customerId = customerId });

        }
    }
}
