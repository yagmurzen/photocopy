using AutoMapper;
using Photocopy.Core.Interface.Repository;
using Photocopy.Core.Interface.Services;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Service.Services
{
    public class CustomerService : ICustomerService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public CustomerDto GetCustomerById(int customerId)
        {
            Customer customer = _unitOfWork.Customers.Find(x => x.Id == customerId && x.IsDeleted == false).Single() ?? new Customer();
            IList<CustomerAddress> customerAddress = _unitOfWork.CustomerAddresses.GetAll(x => x.CustomerId == customer.Id && !x.IsDeleted).ToList();

            CustomerDto outModel = _mapper.Map<CustomerDto>(customer);
            outModel.CustomerAdres = _mapper.Map<IList<CustomerAddressDto>>(customerAddress);
            return outModel;
        }

        public CustomerDto GetCustomer(int customerId)
        {
            Customer customer = _unitOfWork.Customers.Find(x => x.Id == customerId && x.IsDeleted==false).Single() ?? new Customer();
            IList<CustomerAddress> customerAddress = _unitOfWork.CustomerAddresses.GetAll(x => x.Id == customer.Id && !x.IsDeleted).ToList();

            CustomerDto outModel = _mapper.Map<CustomerDto>(customer);
            outModel.CustomerAdres = _mapper.Map<IList<CustomerAddressDto>>(customerAddress);
            return outModel;
        }

        public async Task<CustomerDto> SaveOrUpdate(CustomerDto customer)
        {
            Customer inModel = _mapper.Map<Customer>(customer);

            if (customer.Id!=null)
                await _unitOfWork.Customers.Update(inModel);
            else
                await _unitOfWork.Customers.AddAsync(inModel);

            _unitOfWork.CommitAsync();


            return _mapper.Map<CustomerDto>(inModel);

        }

        public async Task<CustomerAddressDto> SaveOrUpdateAddress(CustomerAddressDto customer)
        {
            CustomerAddress inModel = _mapper.Map<CustomerAddress>(customer);

            await _unitOfWork.CustomerAddresses.AddAsync(inModel);

            return _mapper.Map<CustomerAddressDto>(inModel);

        }
        public IList<CustomerListDto> GetCustomerList()
        {
            var customers = _unitOfWork.Customers.GetAllAsync(x=>!x.IsDeleted).Result.ToList();
            IList<CustomerListDto> customerlist = _mapper.Map<IList<CustomerListDto>>(customers);
            return customerlist;
        }

        public void DeleteCustomer(int customerId)
        {
            _unitOfWork.Customers.Remove(new Customer { Id = customerId });
        }
        public void DeleteCustomerAddress(int addressId)
        {
            _unitOfWork.CustomerAddresses.Remove(new CustomerAddress { Id = addressId });
        }

    }
}
