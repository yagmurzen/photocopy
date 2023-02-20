using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Core.Interface.Services
{
    public interface ICustomerService
    {
        CustomerDto GetCustomerById(int customerId);

        Task<CustomerDto> SaveOrUpdate(CustomerDto customer);
        Task<CustomerAddressDto> SaveOrUpdateAddress(CustomerAddressDto customer);

        IList<CustomerListDto> GetCustomerList();
        void DeleteCustomer(int customerId);
        void DeleteCustomerAddress(int addressId);
    }
}
