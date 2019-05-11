using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    public class InventoryModule
    {
        public List<ProductDetails> productDetails { get; set; }
        public List<VendorDetails> vendorDetails { get; set; }
    }
}
