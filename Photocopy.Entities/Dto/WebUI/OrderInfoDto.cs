using Photocopy.Entities.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Dto.WebUI
{
    public class OrderInfoDto
    {
        public int OrderId { get; set; }
        public CustomerInfoDto Customer { get; set; }
        public string CargoCompanyName { get; set; }
        public string CargoCompanyPrice { get; set; }

        public string PaymentType { get; set; }
        public PaymentState PaymentState { get; set; }
        public OrderState OrderState { get; set; }
        public int OrderStateId { get; set; }


        public string Notes { get; set; }

		public decimal TotalPrice { get; set; }
        public IList<OrderDetailDto> OrderDetail { get; set; }
    }

    public class CustomerInfoDto
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Adrress { get; set; }
        public string City { get; set; }
        public string CityId { get; set; }
    }
    public class OrderDetailDto
    {
        public int OrderDetailId { get; set; }
        public string FileName { get; set; }
        public string Properties { get; set; }
        public string PrintDetail { get; set; }
        public string PriceDetail { get; set; }
        public decimal Price { get; set; }

    }

    public enum PaymentState
    { 
        NotPayment = 0,
        ToDo = 1,
        Done = 2,
    }

   
}
