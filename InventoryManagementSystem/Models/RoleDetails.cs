using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    [DataContract]
    public class RoleDetails
    {
          [DataMember ]
          public int RoleID { get; set; }
          [DataMember]
          public string RoleName { get; set; }
          [DataMember]
          public string CreatedBy { get; set; }
          [DataMember]
          public string Message { get; set; }
          [DataMember]
          public string Action { get; set; }


    }
}
