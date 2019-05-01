﻿using System;
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

            string LotNumber=bdac.GetLotNumber(ProductID);
            DataTable table = bdac.InsertBarcode(ProductID, Convert.ToInt32(Quantity), LotNumber);
            barcode = bdac.GetBarcodeList(table,LotNumber,ProductID);


            return Ok(barcode);
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

    }
}