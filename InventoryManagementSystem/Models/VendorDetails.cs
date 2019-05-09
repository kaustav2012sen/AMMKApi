using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    [DataContract]
    public class VendorDetails
    {
        [DataMember]
        public int VendorID { get; set; }
        [DataMember]
        public string VendorName { get; set; }
        [DataMember]
        public string VendorLocation { get; set; }
        
    }
}
