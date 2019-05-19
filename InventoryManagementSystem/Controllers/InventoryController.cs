using System;
using System.Collections.Generic;
using System.Data;
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
    public class InventoryController : ControllerBase
    {

        [HttpPost]
        [ActionName("GenerateBarcodeByLot")]
        [Route("[action]")]
        public ActionResult GenerateBarcodeByLot([FromBody] JObject postBody)
        {
            BarcodeDataAccessLayer bdac = new BarcodeDataAccessLayer();
            List<Barcode> barcode = new List<Barcode>();
            //UserDetails ud = new UserDetails();
            string ProductID = postBody["ProductID"].ToString();
            string Quantity = postBody["Quantity"].ToString();
            string VendorID = postBody["VendorID"].ToString();
            string UserID = postBody["UserID"].ToString();

            string LotNumber=bdac.GetLotNumber(ProductID);
            DataTable table = bdac.InsertBarcode(ProductID, Convert.ToInt32(Quantity), LotNumber, VendorID);
            barcode = bdac.GetBarcodeList(table,LotNumber,ProductID,VendorID ,UserID);


            return Ok(barcode);
        }


        [HttpPost]
        [ActionName("GetProductVendorList")]
        [Route("[action]")]
        public ActionResult GetProductVendorList()
        {
            ProductDetailAccessLayer pdac = new ProductDetailAccessLayer();

            InventoryModule inventoryModule = new InventoryModule();
            inventoryModule = pdac.GetInventoryModule();
              

            return Ok(inventoryModule);
        }



        [HttpPost]
        [ActionName("UpdateStock")]
        [Route("[action]")]
        public ActionResult UpdateStock([FromBody] JObject postBody)
        {

            //UserDetails ud = new UserDetails();
            string BarcodeNumber = postBody["BarcodeNumber"].ToString();
            string StockAction = postBody["StockAction"].ToString();

            #region Action Definition
            //Stock Action = 1 -- Generate Barcode
            //Stock Action = 2 -- Stock Out
            //Stock Action = 3 -- Stock In
            #endregion



            return Ok();
        }


        [HttpPost]
        [ActionName("UpdateStockForSale")]
        [Route("[action]")]
        public ActionResult UpdateStockForSale([FromBody] JObject postBody)
        {

            //UserDetails ud = new UserDetails();
            string BillingName = postBody["BillingName"].ToString();
            string Quantity = postBody["Quantity"].ToString();


            return Ok();
        }

        [HttpPost]
        [ActionName("StockUpdate")]
        [Route("[action]")]
        public ActionResult StockUpdate([FromBody] JObject postBody)
        {
            BarcodeDataAccessLayer bdac = new BarcodeDataAccessLayer();
            List<Barcode> barcode = new List<Barcode>();
            //UserDetails ud = new UserDetails();
              
            string UserID = postBody["UserID"].ToString(); // added by gautam
            string BarcodeNumber = postBody["BarcodeNumber"].ToString();
            string StockAction = postBody["StockAction"].ToString();

            List<StockStatus> ss1 = new List<StockStatus>();
            ss1 = bdac.StockInitialization(BarcodeNumber, StockAction,UserID);
                        
            return Ok(ss1);
        }
    }
}