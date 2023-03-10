using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photocopy.Entities.Domain;

namespace Photocopy.Entities.Dto
{
    public class ContactDto
    {
        public virtual string Fullname { get; set; }
        public virtual string Email { get; set; }
        public virtual string Message { get; set; }
        public virtual string PhoneNumber { get; set; }
    }
}
