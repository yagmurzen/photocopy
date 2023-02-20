using Photocopy.Entities.Dto;
using Photocopy.Entities.Dto.WebUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Domain
{
    public class Order :EntityBase
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public decimal TotalPrice { get; set; }
        public int CargoCompanyId { get; set; }
        public virtual CargoFirm CargoCompany { get; set; }

        public int PaymentType { get; set; }   
        public string Notes { get; set; }

        public DateTime? TransactionDate { get; set; }
        public int OrderStateId { get; set; }
        public virtual OrderState OrderState { get; set; }

        public PaymentState PaymentState { get; set; }


    }
}
