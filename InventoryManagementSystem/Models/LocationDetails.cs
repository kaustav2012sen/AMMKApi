using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    [DataContract]
    public class LocationDetails
    {
        [DataMember]
        public string LocationName { get; set; }
       [DataMember ]
        public int LocationID { get; set; }
        [DataMember]
        public string Action { get; set; }
    }
}
