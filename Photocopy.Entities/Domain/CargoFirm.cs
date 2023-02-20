﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Domain
{
    public class CargoFirm : EntityBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public virtual Order Order { get; set; }

    }
}
