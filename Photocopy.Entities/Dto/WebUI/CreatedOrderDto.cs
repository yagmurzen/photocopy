using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Dto.WebUI
{
    public class CreatedOrderDto
    {
        public int? Id { get; set; }
        public CustomerDto Customer { get; set; }
        public string CargoCompany { get; set; }
        public string PaymentType { get; set; }
        public string Notes { get; set; }
        public decimal TotalPrice { get; set; }
        public IList<CalculateDto> OrderDetails { get; set; }
    }

    public class CustomerDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public AddressDto Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class AddressDto
    {
        public int? Id { get; set; }
        public int CustomerId { get; set; }
        public string Address { get; set; }
        public string CityCode { get; set; }
        public string DistrictCode { get; set; }


    }
}