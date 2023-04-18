using AutoMapper;
using Photocopy.Core.Interface.Repository;
using Photocopy.Core.Interface.Services;
using Photocopy.DataAccess.Repository;
using Photocopy.Entities.Domain;
using Photocopy.Entities.Domain.Domain;
using Photocopy.Entities.Dto;
using Photocopy.Entities.Dto.WebUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerDto = Photocopy.Entities.Dto.WebUI.CustomerDto;
using UploadDataDto = Photocopy.Entities.Dto.WebUI.UploadDataDto;

namespace Photocopy.Service.Services
{
    public class OrderService : IOrderService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public decimal PagePrice { get; private set; }

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IList<OrderListDto> GetOrders()
        {
            var ss = _unitOfWork.Orders.GetAllOrder(x => !x.IsDeleted);
            IList<OrderListDto> list = _mapper.Map<IList<OrderListDto>>(ss);

            return list;

        }

        public OrderListDto GetOrderDetail(int orderId)
        {

            OrderListDto orderData = _mapper.Map<OrderListDto>(_unitOfWork.Orders.Find(x => !x.IsDeleted && x.Id == orderId).Single() ?? new Order());
            orderData.OrderDetails = _mapper.Map<IList<Entities.Dto.OrderDetailDto>>(_unitOfWork.OrderDetails.GetAll(x => !x.IsDeleted && x.OrderId == orderId).ToList());
            //orderData.OrderStateText = _unitOfWork.Orders.GetOrderState(x=>x.Id== orderData.OrderStateId).Text;
            return orderData;

        }
       
        public async Task<OrderListDto> SaveOrUpdateOrder(OrderListDto order)
        {
            Order orderDbModel = _mapper.Map<Order>(order);
            if (order.Id == 0)
                _unitOfWork.Orders.AddAsync(orderDbModel);
            else
                _unitOfWork.Orders.Update(orderDbModel);

            await _unitOfWork.CommitAsync();

            return _mapper.Map<OrderListDto>(orderDbModel);
        }


        public CalculateDto Calculate(CalculateDto inModel)
        {
            decimal total = 0;
            IList<LookupPrice> lookupPrice= _unitOfWork.Orders.GetAllLookupPrice().Result.ToList();

            decimal formatPrice= (Decimal)lookupPrice.Where(x => x.TagValue == inModel.Format.ToString()).Where(x=> x.IsColourful == inModel.Colourfull).Where(x =>  x.IsQuality == inModel.Quality).Take(1).Single().DefaultValue;

            decimal sidePrice = (Decimal)lookupPrice.Where(x => x.TagValue == inModel.Side.ToString()).Take(1).Single().DefaultValue;

            decimal rotatePrice = (Decimal)lookupPrice.Where(x => x.TagValue == inModel.Rotate.ToString()).Take(1).Single().DefaultValue;

            decimal combinationPrice = (Decimal) lookupPrice.Where(x => x.TagValue == "1"+inModel.Combination.ToString()).Take(1).Single().DefaultValue;

            decimal paperType = (Decimal)lookupPrice.Where(x => x.TagValue == inModel.PaperType.ToString()).Take(1).Single().DefaultValue;

            decimal binding = (Decimal)lookupPrice.Where(x => x.TagValue == inModel.Binding.ToString()).Take(1).Single().DefaultValue;

            total = formatPrice + sidePrice + rotatePrice + combinationPrice +paperType + binding;



            inModel.PagePrice = formatPrice;
            inModel.UnitPrice = total * inModel.PdfPageCount;
            inModel.FilePrice = total * inModel.Count * inModel.PdfPageCount;
            inModel.TotalPrice = total;

            return inModel;
           
        }

        public async Task<CustomerDto> CreateCustomerAsync(CustomerDto model)
        {
            Customer customer = _mapper.Map<Customer>(model);
            customer.CreatedAt= DateTime.Now;
            Customer dbCustomer = _unitOfWork.Customers.Find(x => x.Email == model.Email && x.PhoneNumber == model.PhoneNumber).FirstOrDefault() ?? new Customer();
            
            if (dbCustomer.Id==0) await _unitOfWork.Orders.CreateCustomer(customer);
            else await _unitOfWork.Orders.UpdateCustomer(customer);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task CreateCustomerAddressAsync(AddressDto model)
        {
            CustomerAddress address = _mapper.Map<CustomerAddress>(model);
            await _unitOfWork.Orders.CreateCustomerAddress(address);
            await _unitOfWork.CommitAsync();
        }

        public async Task<CreatedOrderDto> CreateOrderAsync(CreatedOrderDto model)
        {

            decimal totalPrice = model.OrderDetails.Sum(x => x.FilePrice);

            LookupPrice cargo_price = _unitOfWork.Orders.GetLookupPriceByType(x => x.Tag == "Cargo" && x.Text== model.CargoCompany);

            Order order = new Order
            {
                CustomerId = model.Customer.Id.Value,
                Notes = model.Notes,
                PaymentType = 1,
                CargoCompanyId = 1,
                TotalPrice = totalPrice + (decimal)cargo_price.DefaultValue,
                TransactionDate = DateTime.Now,
                OrderStateId = 1
            };

            await _unitOfWork.Orders.CreateOrder(order);
            
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CreatedOrderDto>(order);

        }

        public async Task CreateOrderDetailAsync(CreatedOrderDto model)
        {
            IList<OrderDetail> list = new List<OrderDetail>();
            foreach (var item in model.OrderDetails)
            {
                OrderDetail orderdetails = new OrderDetail
                {
                    //UploadDataId =item.UploadDataId,
                    Format = item.Format,
                    Binding = item.Binding,
                    PaperType = item.PaperType,
                    PdfPageCount = item.PdfPageCount,
                    Colourfull = item.Colourfull,
                    Combination = item.Combination,
                    Count = item.Count,
                    Quality = item.Quality,
                    Rotate = item.Rotate,
                    Side = item.Side,
                    OrderId = model.Id.Value,
                    UploadDataId= item.UploadDataId,
                    UnitPrice= item.UnitPrice,
                    PagePrice= item.PagePrice,
                    FilePrice= item.FilePrice                    
                };
                list.Add(orderdetails);
            }


            await _unitOfWork.Orders.CreateOrderDetail(list);
            await _unitOfWork.CommitAsync();

        }

        public async Task<Guid> UploadDataAsync(UploadDataDto inModel)
        {
            UploadData data = new UploadData { FilePath = inModel.FileData, FileName=inModel.FileName  };
            await _unitOfWork.Orders.UploadData(data);
            await _unitOfWork.CommitAsync();

            return data.Id;
        }
        public Entities.Dto.UploadDataDto GetUploadData(Guid id)
        {
            return _mapper.Map<Entities.Dto.UploadDataDto>( _unitOfWork.Orders.GetUploadData(id));
        }

        public CustomerDto FindCustomer(CustomerDto customerDto)
        {
            Customer? customer = _unitOfWork.Customers.Find(x => x.Email == customerDto.Email && x.PhoneNumber==customerDto.PhoneNumber).SingleOrDefault();

            CustomerDto outModel = _mapper.Map<CustomerDto>(customer);

            return outModel;
        }

        public OrderInfoDto GetOrderInfo(int id)
        {
            Order? order = _unitOfWork.Orders.GetAllOrder(x => x.Id==id).FirstOrDefault();
            if (order == null) return null;
            Customer customer = _unitOfWork.Customers.Find(x => x.Id==order.CustomerId).Single()?? new Customer();

            CustomerAddress customerAddress = _unitOfWork.CustomerAddresses.GetCustomerAddress(x => x.CustomerId == order.CustomerId);

            IList<OrderDetail> orderDetail = _unitOfWork.OrderDetails.GetOrderDetails(x => x.OrderId == order.Id).ToList();

            IList<Entities.Dto.WebUI.OrderDetailDto> details= new List<Entities.Dto.WebUI.OrderDetailDto>();

            foreach (var detail in orderDetail)
            {

                details.Add(new Entities.Dto.WebUI.OrderDetailDto
                {
                    OrderDetailId = detail.OrderId,
                    FileName = detail.UploadData.FileName,
                    Price = detail.FilePrice,
                    PriceDetail = "Sayfa Tutarı: " + detail.PagePrice + " TL Adet Tutarı:" + detail.UnitPrice + " TL",
                    Properties = detail.Format + " - 1 - " + detail.Side.ToString() + " - " + (detail.Colourfull==0 ?"Siyah Beyaz":"Renkli") + " - " + (detail.Quality==0 ? "Standart":"Kaliteli") + " - " + detail.Binding,
                    PrintDetail = detail.PdfPageCount + " Sayfa X " + detail.Count
                });              
            
            }

            return new OrderInfoDto
            {
                OrderId = order.Id,
                CargoCompanyName = order.CargoCompany.Name.ToString(),
                CargoCompanyPrice = order.CargoCompany.Price.ToString(),
                PaymentType = order.PaymentType.ToString(),
                TotalPrice = order.TotalPrice,
                Customer = new CustomerInfoDto
                {   Name = customer.Name, 
                    LastName = customer.LastName, 
                    PhoneNumber = customer.PhoneNumber, 
                    Email= customer.Email,
                    Adrress = customerAddress.Address, 
                    City = customerAddress.City.CityName.ToString() 
                },
                OrderDetail= details,
                Notes= order.Notes,
                OrderState= order.OrderState
            };
        }

        public async Task SetOrderDetail(OrderListDto orderInfoDto)
        {
            Order  order=_unitOfWork.Orders.Find(x=>x.Id== orderInfoDto.Id).FirstOrDefault() ?? new Order();
            order.OrderStateId = orderInfoDto.OrderStateId==0? order.OrderStateId : orderInfoDto.OrderStateId; 
            order.PaymentState = (PaymentState)orderInfoDto.PaymentState;
            order.TransactionDate =  DateTime.Now;
            order.OrderInvoiceId = orderInfoDto.OrderInvoiceId;
            order.OrderInvoiceDetailId = orderInfoDto.OrderInvoiceDetailId;
            order.ShipperBranchCode = orderInfoDto.ShipperBranchCode;

            await _unitOfWork.Orders.Update(order);
            await _unitOfWork.CommitAsync();
        }
        public async Task SetOrderComplated(OrderListDto orderInfoDto)
        {
            Order order = _unitOfWork.Orders.Find(x => x.Id == orderInfoDto.Id).FirstOrDefault() ?? new Order();
            order.OrderStateId = orderInfoDto.OrderStateId;
            order.PaymentState = (PaymentState)orderInfoDto.PaymentState;
            order.TransactionDate = DateTime.Now;

            await _unitOfWork.Orders.Update(order);
            await _unitOfWork.CommitAsync();
        }

        public async Task SetLookupPrice(SetLookupPriceCmsDto  lookupPriceCmsDto)
        {
            LookupPrice price = _unitOfWork.Orders.GetLookupPrice(x => x.Id == lookupPriceCmsDto.Id);
            price.DefaultValue = lookupPriceCmsDto.DefaultValue;

            await _unitOfWork.Orders.UpdateLookupPrice(price);
            await _unitOfWork.CommitAsync();
        }
        public IList<OrderStateItemDto> GetAllOrderState()
        {

            return  _mapper.Map<IList<OrderStateItemDto>>(_unitOfWork.Orders.GetAllOrderState(x => !x.IsDeleted).Result.ToList()) ;
        }

        public string GetOrderStatus(string siparisNo, string telefon)
        {
            Order? order = _unitOfWork.Orders.GetOrderStatus(x => !x.IsDeleted && x.Id == Convert.ToInt64(siparisNo) && x.Customer.PhoneNumber == telefon);
            string state = order != null ? order.OrderState.Text : "Sipariş Bulunamadı. ";

			return state;
        }
        public async Task RemoveOrder(int id,bool isDeleted)
        {
            Order order = _unitOfWork.Orders.Find(x => x.Id == id).FirstOrDefault() ?? new Order();
            order.IsDeleted = isDeleted;
            await _unitOfWork.Orders.Update(order);

            await _unitOfWork.CommitAsync();
        }

        public IList<LookupPriceListDto> GetAllLookupPrice()
        {
            return _mapper.Map<IList<LookupPriceListDto>>(_unitOfWork.Orders.GetAllLookupPrice().Result.ToList());
        }

    }
}
