using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photocopy.Entities.Domain;

namespace Photocopy.Entities.Domain
{
    public class PaymentLog : EntityBase
    {
        public virtual int OrderId { get; set; }

        public virtual string RequestData { get; set; }

        public virtual string ResponseData { get; set; }

    }
}
