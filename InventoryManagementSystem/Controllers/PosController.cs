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
        [ActionName("GenerateBill")]
        [Route("[action]")]
        public ActionResult GenerateBill([FromBody] List<JArray> postBody)
        {
            int listcount, dcount;
            string BillingName, ProductName, Barcodenumber;
            string UserID = "";
            int HNScode;
            double GrossBillValue = 0, TotalGSTValue = 0, GSTValue = 0, NetBillValue = 0, Rate, GST, Cost, DiscountAmount = 0, CashAmount = 0, CardAmount = 0, CGSTValue = 0, SGSTValue = 0, IGSTValue = 0, BillPendingAmount = 0;
            int Discount = 0, CardNumber = 0, TransactionType = 0;
            bool PendingBill = false;

            listcount = postBody[0].Count;
            dcount = postBody[1].Count;
            DataTable dtBillDetails;
            dtBillDetails = new DataTable();

            List<PosDetails> posDetails = new List<PosDetails>();

            dtBillDetails.Columns.Add("ProductBillingName", typeof(string));
            dtBillDetails.Columns.Add("ProductName", typeof(string));
            dtBillDetails.Columns.Add("Barcode", typeof(string));
            dtBillDetails.Columns.Add("HSN", typeof(int));
            dtBillDetails.Columns.Add("Rate", typeof(double));
            dtBillDetails.Columns.Add("GSTPercent", typeof(double));
            dtBillDetails.Columns.Add("TotalValue", typeof(double));
            dtBillDetails.Columns.Add("CGSTValue", typeof(double));
            dtBillDetails.Columns.Add("SGSTValue", typeof(double));
            dtBillDetails.Columns.Add("IGSTValue", typeof(double));
            dtBillDetails.Columns.Add("ItemGSTValue", typeof(double));

            //Bill Details
            for (int i = 0; i < listcount; i++)
            {
                BillingName = postBody[0][i]["BillingName"].ToString();
                ProductName = postBody[0][i]["ProductName"].ToString();
                Barcodenumber = postBody[0][i]["BarcodeNumber"].ToString();
                HNScode = Convert.ToInt32(postBody[0][i]["HSN"]);
                Rate = Convert.ToDouble(postBody[0][i]["Rate"]);
                GST = Convert.ToDouble(postBody[0][i]["GST"]);
                Cost = Convert.ToDouble(postBody[0][i]["Cost"]);
                CGSTValue = Convert.ToDouble(postBody[0][i]["CGSTValue"]);
                SGSTValue = Convert.ToDouble(postBody[0][i]["SGSTValue"]);
                IGSTValue = Convert.ToDouble(postBody[0][i]["IGSTValue"]);
                GSTValue = Convert.ToDouble(postBody[0][i]["GSTValue"]);
                //GSTValue = (Cost - Rate);

                DataRow dr = dtBillDetails.NewRow();

                dr["ProductBillingName"] = BillingName;
                dr["ProductName"] = ProductName;
                dr["Barcode"] = Barcodenumber;
                dr["HSN"] = HNScode;
                dr["Rate"] = Rate;
                dr["GSTPercent"] = GST;
                dr["TotalValue"] = Cost;
                dr["CGSTValue"] = CGSTValue;
                dr["SGSTValue"] = SGSTValue;
                dr["IGSTValue"] = IGSTValue;
                dr["ItemGSTValue"] = GSTValue;

                dtBillDetails.Rows.Add(dr);
            }

            //Bill Summary
            for (int j = 0; j < dcount; j++)
            {
                NetBillValue = Convert.ToDouble(postBody[1][j]["NetBillValue"]);
                GrossBillValue = Convert.ToDouble(postBody[1][j]["GrossBillValue"]);
                TotalGSTValue = Convert.ToDouble(postBody[1][j]["TotalGSTValue"]);
                DiscountAmount = Convert.ToDouble(postBody[1][j]["DiscountAmount"]);
                TransactionType = Convert.ToInt32(postBody[1][j]["TransactionType"]);
                CashAmount = Convert.ToDouble(postBody[1][j]["CashAmount"]);
                CardAmount = Convert.ToDouble(postBody[1][j]["CardAmount"]);
                CardNumber = Convert.ToInt32(postBody[1][j]["CardNumber"]);
                UserID = Convert.ToString(postBody[1][j]["UserID"]);
                PendingBill = Convert.ToBoolean(postBody[1][j]["PendingBill"]);
                BillPendingAmount = Convert.ToDouble(postBody[1][j]["BillPendingAmount"]);
            }

            posDetails = bdac.BillDetails(listcount, GrossBillValue, Discount, TotalGSTValue, NetBillValue, dtBillDetails, DiscountAmount, TransactionType, CashAmount, CardAmount, CardNumber, UserID, PendingBill, BillPendingAmount);

            return Ok(posDetails);
        }

        #endregion


    }
}