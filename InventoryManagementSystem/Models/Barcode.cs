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
        public string ProductID { get; set; }
        [DataMember]
        public string BarcodeNumber{get;set;}
        [DataMember]
        public string LotDate { get; set; }

        public static implicit operator Barcode(ProductDetails v)
        {
            throw new NotImplementedException();
        }
    }
}
