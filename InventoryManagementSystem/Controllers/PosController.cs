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
    public class PosController : ControllerBase
    {
        BarcodeDataAccessLayer bdac = new BarcodeDataAccessLayer();

        #region //Barcode status Check in Product for POS 
        [HttpPost]
        [ActionName("PosBarcodeCheck")]
        [Route("[action]")]
        public ActionResult PosBarcodeCheck([FromBody] JObject postBody)
        {
            PosDetails posDetails = new PosDetails();
            string BarcodeNumber = postBody["BarcodeNumber"].ToString();
            posDetails = bdac.PosInitialization(BarcodeNumber); //Definition in BarcodeDataAccessLayer
            return Ok(posDetails);
        }
        #endregion

        #region // Generate bill with all the details, right now there is one array is passed for the product list,
        // 2nd array need to be passed with the totaling the values

        [HttpPost]
        [ActionName("GeneratBill")]
        [Route("[action]")]
        public ActionResult GeneratBill([FromBody] List<JObject> postBody)
        {
            int listcount;
            string BillingName, ProductName, Barcodenumber;
            int HNScode;
            double GrossBillValue = 0, TotalGSTValue = 0, GSTValue, NetBillValue = 0, BillDiscount = 0, Rate, GST, Cost;

            listcount = postBody.Count;

            DataTable dtBillDetails;
            dtBillDetails = new DataTable();
            PosDetails posDetails = new PosDetails();

            dtBillDetails.Columns.Add("ProductBillingName", typeof(string));
            dtBillDetails.Columns.Add("ProductName", typeof(string));
            dtBillDetails.Columns.Add("Barcode", typeof(string));
            dtBillDetails.Columns.Add("HSN", typeof(int));
            dtBillDetails.Columns.Add("Rate", typeof(double));
            dtBillDetails.Columns.Add("GSTPercent", typeof(double));
            dtBillDetails.Columns.Add("TotalValue", typeof(double));

            for (int i = 0; i < listcount; i++)
            {
                BillingName = postBody[i]["BillingName"].ToString();
                ProductName = postBody[i]["ProductName"].ToString();
                Barcodenumber = postBody[i]["BarcodeNumber"].ToString();
                HNScode = Convert.ToInt32(postBody[i]["HSN"]);
                Rate = Convert.ToDouble(postBody[i]["Rate"]);
                GST = Convert.ToDouble(postBody[i]["GST"]);
                Cost = Convert.ToDouble(postBody[i]["Cost"]);
                GSTValue = (Cost - Rate);

                GrossBillValue += Rate;
                TotalGSTValue += GSTValue;
                NetBillValue += Cost;
                DataRow dr = dtBillDetails.NewRow();

                dr["ProductBillingName"] = BillingName;
                dr["ProductName"] = ProductName;
                dr["Barcode"] = Barcodenumber;
                dr["HSN"] = HNScode;
                dr["Rate"] = Rate;
                dr["GSTPercent"] = GST;
                dr["TotalValue"] = Cost;

                dtBillDetails.Rows.Add(dr);
            }

            NetBillValue = GrossBillValue - (GrossBillValue * BillDiscount / 100);
            posDetails = bdac.BillDetails(listcount, GrossBillValue, BillDiscount, TotalGSTValue, NetBillValue, dtBillDetails);

            return Ok(posDetails);
        }

        #endregion


    }
}