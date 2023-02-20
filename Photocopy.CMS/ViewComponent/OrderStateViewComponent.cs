using Microsoft.AspNetCore.Mvc;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities.Dto;

namespace Photocopy.CMS.Component
{
    public class OrderStateViewComponent : ViewComponent
    {
		private IOrderService _service;

		public OrderStateViewComponent(IOrderService service)
		{
			_service = service;
		}

		public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
			OrderStateDto orderStates = new OrderStateDto { Items = _service.GetAllOrderState(), Value = Id } ;
            return View(orderStates);
        }
    }
}
