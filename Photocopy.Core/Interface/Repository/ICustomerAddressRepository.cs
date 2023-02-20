using Photocopy.Entities.Domain;
using System.Linq.Expressions;

namespace Photocopy.Core.Interface.Repository
{
    public interface ICustomerAddressRepository : IRepository<CustomerAddress>
    {
        CustomerAddress GetCustomerAddress(Expression<Func<CustomerAddress, bool>> value);
    }
}
