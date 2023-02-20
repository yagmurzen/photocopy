using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photocopy.Entities.Domain;

namespace Photocopy.Entities.Dto
{
    public class SliderDto
    {
        public virtual int? Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ButtonText { get; set; }
        public virtual string ImagePath { get; set; }
        public virtual string MobileImagePath { get; set; }
        public virtual string Redirect { get; set; }
        public virtual bool IsExternal { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual int Position { get; set; }
        public virtual string Description { get; set; }
        public virtual string Subtitle { get; set; }
    }
}
