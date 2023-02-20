using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Domain
{
    public class District 
    {
        public virtual string Id { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? DeletedAt { get; set; }
        public virtual bool IsDeleted { get; set; }
        public string DistrictName { get; set; }
        public string CityId { get; set; }
        public virtual City City { get; set; }
    }
}
