using Microsoft.AspNetCore.Http;
using Photocopy.Entities.Dto.WebUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Domain
{
    public class OrderDetail :EntityBase
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public Guid UploadDataId { get; set; }
        public virtual UploadData UploadData { get; set; }


        public Format Format { get; set; }

        public Combination Combination { get; set; }
        public Side Side { get; set; }
        public Rotate Rotate { get; set; }

        public int Colourfull { get; set; }
        public int Quality { get; set; }

        public Binding Binding { get; set; }
        public PaperType PaperType { get; set; }
        public int PdfPageCount { get; set; }
        public int Count { get; set; }

        public decimal FilePrice { get; set; }
        public decimal PagePrice { get; set; }
        public decimal UnitPrice { get; set; }


    }
}
