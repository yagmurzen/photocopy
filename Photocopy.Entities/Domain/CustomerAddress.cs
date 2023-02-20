using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Domain
{
    public class CustomerAddress:EntityBase
    {
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public string CityId { get; set; }
        public virtual City City { get; set; }
        public string DistrictId { get; set; }
        public string? Address { get; set; }

    }
}
