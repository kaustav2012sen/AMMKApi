using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    public class PosDetails
    {
        [DataMember]
        public bool ActiveState { get; set; }
        [DataMember]
        public string BarcodeNumber { get; set; }
        [DataMember]
        public string BarcodeStatus { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public float GST { get; set; }
        [DataMember]
        public int HSNCode { get; set; }
        [DataMember]
        public string BillingName { get; set; }
        [DataMember]
        public float Rate { get; set; }
        [DataMember]
        public float Cost { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public float NetBillValue { get; set; }
        [DataMember]
        public float GrossBillValue { get; set; }
        [DataMember]
        public int BillID { get; set; }
        [DataMember]
        public float totalQty { get; set; }
        [DataMember]
        public int Discount { get; set; }
        [DataMember]
        public string BillNumber { get; set; }
        [DataMember]
        public float DiscountAmount { get; set; }
        [DataMember]
        public float CGSTValue { get; set; }
        [DataMember]
        public float SGSTValue { get; set; }
        [DataMember]
        public float IGSTValue { get; set; }
        [DataMember]
        public float ItemGSTValue { get; set; }
        [DataMember]
        public int GSTpercent { get; set; }
        [DataMember]
        public string BillDate { get; set; }
        [DataMember]
        public float BillPendingAmount { get; set; }
    }
}
