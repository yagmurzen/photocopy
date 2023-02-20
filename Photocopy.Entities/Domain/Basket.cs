using Photocopy.Entities.Dto;
using Photocopy.Entities.Dto.WebUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Domain
{
    public class Basket : EntityBase
    {
        public ICollection<BasketDetail> BasketDetails { get; set; }

        public DateTime? TransactionDate { get; set; }

    }
}
