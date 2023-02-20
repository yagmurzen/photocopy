using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photocopy.Entities.Domain;

namespace Photocopy.Entities.Dto
{
    public class FaqDto 
    {
        public virtual int? Id { get; set; }

        public virtual string Question { get; set; }
        public virtual string Answer { get; set; }
        public virtual int Position { get; set; }
        public virtual string SubQuestion { get; set; }
    }
}
