﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Domain
{
    public class OrderState : EntityBase
    {
        public Order Order { get; set; }
        public string Text { get; set; }
        public int Value { get; set; }

    }
}
