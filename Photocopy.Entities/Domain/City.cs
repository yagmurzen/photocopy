using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Domain
{
    public class City 
    {
        public virtual string Id { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? DeletedAt { get; set; }
        public virtual bool IsDeleted { get; set; }
        public string CityName { get; set; }
        public ICollection<District> Districts { get; set; }
        public virtual CustomerAddress CustomerAddress { get; set; }

    }
}
