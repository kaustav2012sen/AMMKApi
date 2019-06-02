using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    public class ProductLotDetails
    {
        [DataMember]
        public int LotNumber { get; set; }
    }
}
