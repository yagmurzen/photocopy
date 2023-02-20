using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Dto.WebUI
{
    public class DistrictDto 
    {
        public virtual string code { get; set; }
        public virtual string name { get; set; }
        public string cityName { get; set; }
        public string cityCode { get; set; }

    }
}
