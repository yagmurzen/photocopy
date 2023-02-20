using Photocopy.Entities.Domain;
using Photocopy.Entities.Domain.Domain;
using Photocopy.Entities.Dto.WebUI;
using System.Collections;
using System.Linq.Expressions;

namespace Photocopy.Core.Interface.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> GetAllOrder(Expression<Func<Order, bool>> predicate);
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer> UpdateCustomer(Customer entity);
        Task<CustomerAddress> CreateCustomerAddress(CustomerAddress address);
        Task<Order> CreateOrder(Order model);
        Task CreateOrderDetail(IList<OrderDetail> model);
        Task<IEnumerable<LookupPrice>> GetAllLookupPrice();
        Task<UploadData> UploadData(UploadData model);
        UploadData GetUploadData(Guid id);
        OrderState GetOrderState(Func<OrderState, bool> value);
        Task<IEnumerable<OrderState>> GetAllOrderState(Expression<Func<OrderState, bool>> predicate);
        Order GetOrderStatus(Expression<Func<Order, bool>> predicate);
        LookupPrice GetLookupPriceByType(Func<LookupPrice, bool> value);
        LookupPrice GetLookupPrice(Expression<Func<LookupPrice, bool>> predicate);

        Task<LookupPrice> UpdateLookupPrice(LookupPrice entity);
    }
}
