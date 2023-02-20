using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photocopy.Entities.Domain;

namespace Photocopy.Entities.Dto
{
    public class CustomerListDto
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }

    public class CustomerDto
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public IList<CustomerAddressDto> CustomerAdres { get; set; }
    }


    public class CustomerAddressDto
    {
        public string Id { get; set; }

        public int CustomerId { get; set; }
        public int CityCode { get; set; }
        public int DistrictCode { get; set; }
        public string Address { get; set; }
    }
}
