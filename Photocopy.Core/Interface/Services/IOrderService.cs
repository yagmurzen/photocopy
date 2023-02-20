using Photocopy.Core.Interface.Repository;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto;
using Photocopy.Entities.Dto.WebUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerDto = Photocopy.Entities.Dto.WebUI.CustomerDto;
using OrderDetailDto = Photocopy.Entities.Dto.OrderDetailDto;

namespace Photocopy.Core.Interface.Services
{
    public interface IOrderService
    {
        CalculateDto Calculate(CalculateDto inModel);
        Task<CreatedOrderDto> CreateOrderAsync(CreatedOrderDto model);
        Task CreateOrderDetailAsync(CreatedOrderDto model);
        Task<Guid> UploadDataAsync(Entities.Dto.WebUI.UploadDataDto model);
        Task<CustomerDto> CreateCustomerAsync(CustomerDto model);
        Task CreateCustomerAddressAsync(AddressDto model);
        OrderInfoDto GetOrderInfo(int id);

        IList<OrderListDto> GetOrders();

        OrderListDto GetOrderDetail(int orderId);
        Task<OrderListDto> SaveOrUpdateOrder(OrderListDto order);
        Entities.Dto.UploadDataDto GetUploadData(Guid id);
        Task SetOrderDetail(OrderListDto orderDetailDto);
        Task RemoveOrder(int id, bool isDeleted);

        IList<OrderStateItemDto> GetAllOrderState();
        string GetOrderStatus(string siparisNo, string telefon);
        IList<LookupPriceListDto> GetAllLookupPrice();
        Task SetLookupPrice(SetLookupPriceCmsDto lookupPriceCmsDto);
    }
}
