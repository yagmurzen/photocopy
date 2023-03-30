using Photocopy.Entities.Domain;
using Photocopy.Entities.Dto.WebUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Dto
{
    public class OrderListDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int PaymentState { get; set; }
        public decimal TotalPrice { get; set; }
        public int CargoCompanyId { get; set; }
        public int PaymentType { get; set; }
        public string Notes { get; set; }
        public DateTime TransactionDate { get; set; }

        public IList<OrderDetailDto> OrderDetails { get; set; }
        public int OrderStateId { get; set; }
        public virtual OrderStateItemDto OrderState { get; set; }
        public string OrderInvoiceId { get; set; }
        public string OrderInvoiceDetailId { get; set; }
        public string ShipperBranchCode { get; set; }
    }

    public class OrderDetailDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public Guid UploadDataId { get; set; }

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

    public class OrderStateDto
    {
        public int Value { get; set; }
        public IList<OrderStateItemDto> Items { get; set; }
    }

    public class OrderStateItemDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string Text { get; set; }
    }

}
