using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        ProductDetailAccessLayer pdac = new ProductDetailAccessLayer();

        [HttpPost]
        [ActionName("GetProductList")]
        [Route("[action]")]
        public ActionResult GetProductList()
        {

            List<ProductDetails> pd = new List<ProductDetails>();

            pd = pdac.GetProductList();      


            return Ok(pd);
        }

        [HttpPost]
        [ActionName("GetProductLotInfo")]
        [Route("[action]")]
        public ActionResult GetProductLotInfo([FromBody] JObject postBody)
        {
            int ProductID = Convert.ToInt32(postBody["ProductID"]);

            ProductDetailAccessLayer pdal = new ProductDetailAccessLayer();

            List<ProductLotDetails> productlotdetails = new List<ProductLotDetails>();

            productlotdetails = pdal.ProductLotNumberInfo(ProductID);

            return Ok(productlotdetails);
        }

        [HttpPost]
        [ActionName("LotNumberBarcodeInfo")]
        [Route("[action]")]
        public ActionResult LotNumberBarcodeInfo([FromBody] JObject postBody)
        {
            int ProductID = Convert.ToInt32(postBody["ProductID"]);
            int LotNumber = Convert.ToInt32(postBody["LotNumber"]);

            ProductDetailAccessLayer pdal = new ProductDetailAccessLayer();

            // List<ProductLotDetails> productlotdatails1 = new List<ProductLotDetails>();
            List<Barcode> barcodes = new List<Barcode>();

            barcodes = pdal.ProductLotBarcodeNumbe(ProductID, LotNumber);
            return Ok(barcodes);
        }
    }
}