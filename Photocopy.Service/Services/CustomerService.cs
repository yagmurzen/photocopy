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
            Customer customer = _unitOfWork.Customers.Find(x => x.Id == customerId && x.IsDeleted == false).SingleOrDefault() ?? new Customer();
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

        public async void DeleteCustomer(int customerId)
        {
            Customer customer = _unitOfWork.Customers.GetByIdAsync(x => !x.IsDeleted && x.Id == customerId);
            customer.IsDeleted = true;
            await _unitOfWork.Customers.Update(customer);
            _unitOfWork.CommitAsync();
        }
        public async void DeleteCustomerAddress(int addressId)
        {
            CustomerAddress adres = _unitOfWork.CustomerAddresses.GetByIdAsync(x => !x.IsDeleted && x.Id == addressId);
            adres.IsDeleted = true;
            await _unitOfWork.CustomerAddresses.Update(adres);
            _unitOfWork.CommitAsync();
        }

    }
}
