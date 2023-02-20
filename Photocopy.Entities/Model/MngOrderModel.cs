using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photocopy.Entities.Model
{
    public class MngTokenModel
    {
        public string jwt { get; set; }
        public string refreshToken { get; set; }
        public string jwtExpireDate { get; set; }
        public string refreshTokenExpireDate { get; set; }
    }

    public class MngOrder
    {
        public Order order { get; set; }
        public IList<Orderpiecelist> orderPieceList { get; set; }
        public Recipient recipient { get; set; }
    }

    public class Order
    {
        public string referenceId { get; set; }
        public string barcode { get; set; }
        public string billOfLandingId { get; set; }
        public int isCOD { get; set; }
        public int codAmount { get; set; }
        public int shipmentServiceType { get; set; }
        public int packagingType { get; set; }
        public string content { get; set; }
        public int smsPreference1 { get; set; }
        public int smsPreference2 { get; set; }
        public int smsPreference3 { get; set; }
        public int paymentType { get; set; }
        public int deliveryType { get; set; }
        public string description { get; set; }
        public string marketPlaceShortCode { get; set; }
        public string marketPlaceSaleCode { get; set; }
        public string pudoId { get; set; }
    }

    public class Recipient
    {
        public int? customerId { get; set; }
        public string refCustomerId { get; set; }
        public int cityCode { get; set; }
        public string cityName { get; set; }
        public string districtName { get; set; }
        public int districtCode { get; set; }
        public string address { get; set; }
        public string bussinessPhoneNumber { get; set; }
        public string email { get; set; }
        public string taxOffice { get; set; }
        public string taxNumber { get; set; }
        public string fullName { get; set; }
        public string homePhoneNumber { get; set; }
        public string mobilePhoneNumber { get; set; }
    }

    public class Orderpiecelist
    {
        public string barcode { get; set; }
        public int desi { get; set; }
        public int kg { get; set; }
        public string content { get; set; }
    }

    public class Rootobject
    {
        public MngOrderModel[] Property1 { get; set; }
    }

    public class MngOrderModel
    {
        public string orderInvoiceId { get; set; }
        public string orderInvoiceDetailId { get; set; }
        public string shipperBranchCode { get; set; }
    }

}
