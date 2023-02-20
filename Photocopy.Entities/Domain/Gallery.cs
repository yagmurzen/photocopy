using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Domain
{
    public class Galery: EntityBase
    {
        public string Name { get; set; }
        public virtual ContentNode ContentNode { get; set; }
        public int ContentNodeId { get; set; }
        public  virtual ICollection<GaleryFile> GaleryFiles { get; set; }

    }

    public class GaleryFile : EntityBase
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SubTitle { get; set; }
        public int Position { get; set; }
        public virtual Galery Galery { get; set; }
        public int GaleryId { get; set; }

    }
}
