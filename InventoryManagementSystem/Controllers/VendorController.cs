using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        VendorDetailAccessLayer vdal = new VendorDetailAccessLayer();

        [HttpPost]
        [ActionName("GetVendorList")]
        [Route("[action]")]
        public ActionResult GetVendorList()
        {

            List<VendorDetails> vendorDetails = new List<VendorDetails>();

            vendorDetails = vdal.GetVendorList();


            return Ok(vendorDetails);
        }
    }
}