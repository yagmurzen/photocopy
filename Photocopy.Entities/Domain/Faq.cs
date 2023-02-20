using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photocopy.Entities.Domain;

namespace Photocopy.Entities.Domain
{
    public class Faq : EntityBase
    {
        [StringLength(256, MinimumLength = 1)]
        public virtual string Question { get; set; }

        [StringLength(256, MinimumLength = 1)]
        public virtual string Answer { get; set; }

        public virtual int Position { get; set; }

        [StringLength(256, MinimumLength = 1)]
        public virtual string SubQuestion { get; set; }
    }
}
