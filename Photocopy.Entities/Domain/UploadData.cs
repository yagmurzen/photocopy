using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Domain
{
    public class UploadData 
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; }
        public int IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }
        public virtual BasketDetail BasketDetail { get; set; }

        public string FileName { get; set; }
    }
}
