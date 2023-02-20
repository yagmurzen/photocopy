using Microsoft.EntityFrameworkCore;
using Photocopy.Core.AppContext;
using Photocopy.Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private UserRepository _userRepository;
        private BlogNodeRepository _blogRepository;
        private ContentNodeRepository _contentRepository;
        private FaqRepository _faqRepository;
        private OrderRepository _orderRepository;
        private SliderRepository _sliderRepository;
        private CustomerRepository _customerRepository;
        private CustomerAddressRepository _customerAdressRepository;
        private OrderDetailRepository _orderDetailRepository;
        private CityRepository _cityRepository;
        private DistrictRepository _districtRepository;
        private CargoFirmRepository _cargoFirmRepository;


        private BasketRepository _basketRepository;
        private BasketDetailRepository _basketDetailRepository;

        public UnitOfWork(ApplicationContext context)
        {
            this._context = context;
        }
        public ICityRepository Cities => _cityRepository = _cityRepository ?? new CityRepository(_context);
        public IDistrictRepository Districts => _districtRepository = _districtRepository ?? new DistrictRepository(_context);
        public ICargoFirmRepository CargoFirms => _cargoFirmRepository = _cargoFirmRepository ?? new CargoFirmRepository(_context);

        public IUserRepository Users => _userRepository = _userRepository ?? new UserRepository(_context);

        public IBlogNodeRepository Blogs => _blogRepository= _blogRepository?? new BlogNodeRepository(_context);
        public IContentNodeRepository Contents => _contentRepository = _contentRepository ?? new ContentNodeRepository(_context);

        public ICustomerRepository Customers => _customerRepository = _customerRepository ?? new CustomerRepository(_context);


        public ISliderRepository Sliders => _sliderRepository = _sliderRepository ?? new SliderRepository(_context);
        public IFaqRepository Faqs => _faqRepository = _faqRepository ?? new FaqRepository(_context);
        public IOrderRepository Orders => _orderRepository = _orderRepository ?? new OrderRepository(_context);

        public IOrderDetailRepository OrderDetails => _orderDetailRepository = _orderDetailRepository ?? new OrderDetailRepository(_context);
        public ICustomerAddressRepository CustomerAddresses => _customerAdressRepository = _customerAdressRepository ?? new CustomerAddressRepository(_context);

        public IBasketRepository Baskets => _basketRepository = _basketRepository ?? new BasketRepository(_context);

        public IBasketDetailRepository BasketDetails => _basketDetailRepository = _basketDetailRepository ?? new BasketDetailRepository(_context);


        public async Task<int> CommitAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
