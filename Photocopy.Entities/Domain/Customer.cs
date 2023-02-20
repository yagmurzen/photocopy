using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photocopy.Entities.Domain;

namespace Photocopy.Entities.Domain
{
    public class Customer : EntityBase
    {

        public string Name { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public ICollection<CustomerAddress> CustomerAddress { get; set; }
        public ICollection<Order> Orders { get; set; }


    }


}
