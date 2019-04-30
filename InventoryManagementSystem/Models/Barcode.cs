using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    [DataContract]
    public class Barcode
    {
        [DataMember]
        public string LotNumber { get; set; }
        [DataMember]
        public string BillingName { get; set; }
        [DataMember]
        public string BarcodeNumber{get;set;}
        
    }
}
