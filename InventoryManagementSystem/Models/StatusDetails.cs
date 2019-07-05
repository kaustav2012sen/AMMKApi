using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    [DataContract]
    public class StatusDetails
    {
        [DataMember]
        public string StatusName { get; set; }
        [DataMember]
        public int StatusID { get; set; }
        [DataMember]
        public string Action { get; set; }
    }
}
