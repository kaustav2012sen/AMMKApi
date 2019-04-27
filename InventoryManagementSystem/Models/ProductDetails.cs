using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    [DataContract]
    public class ProductDetails
    {
        [DataMember]
        public int ProductID { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public string BillingName { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public int GST { get; set; }
        [DataMember]
        public int HSNCode { get; set; }
    }
}
