using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    [DataContract]
    public class StockStatus
    {
        [DataMember]
        public string BarcodeNumber { get; set; }
        [DataMember]
        public string BarcodeStatus { get; set; }
        [DataMember]
        public bool ActionCompletion { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}
