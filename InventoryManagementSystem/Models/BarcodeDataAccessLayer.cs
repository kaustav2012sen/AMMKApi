using InventoryManagementSystem.Models.BussinessMethods;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    public class BarcodeDataAccessLayer
    {
        string Connection = Startup.ConnectionString;
        string LotNumber;

        BarcodeGenerator bg = new BarcodeGenerator();

        public string GetLotNumber(string ProductID)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_GetLotNumber", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProductID ", SqlDbType.NVarChar).Value = ProductID;
            cmd.Parameters.Add("@LotNumber", SqlDbType.Int);
            cmd.Parameters["@LotNumber"].Direction = ParameterDirection.Output;
            //var returnParameter = cmd.Parameters.Add("@LotNumber", SqlDbType.Int);
            //returnParameter.Direction = ParameterDirection.ReturnValue;

            cmd.ExecuteNonQuery();
            LotNumber = Convert.ToString(cmd.Parameters["@LotNumber"].Value);

            return LotNumber;

        }

        public DataTable InsertBarcode(string ProductID, int Quantity, string LotNumber, string VendorID)
        {
            DataTable table = new DataTable();
            table = bg.GenerateBarcode(LotNumber, Quantity, ProductID, VendorID);

            return table;
        }

        public List<Barcode> GetBarcodeList(DataTable barcodetable, string LotNumber, string ProductID, string VendorID, string UserID)
        {
            List<Barcode> barCodeDetails = new List<Barcode>();

            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_SetBarcode", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProductIDforRetreival", SqlDbType.Int).Value = Convert.ToInt32(ProductID);
            cmd.Parameters.Add("@LotNumberforRetreival", SqlDbType.Int).Value = Convert.ToInt32(LotNumber);
            cmd.Parameters.Add("@VendorIDforRetreival", SqlDbType.Int).Value = Convert.ToInt32(VendorID);
            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = UserID; //Added by Goutam Giri
            SqlParameter param = cmd.Parameters.AddWithValue("@BarcodeList", barcodetable);

            param.SqlDbType = SqlDbType.Structured;

            //cmd.ExecuteNonQuery();

            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();



            if (dt.Rows.Count > 0)
            {
                Barcode barcode;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    barcode = new Barcode();

                    barcode.ProductID = dt.Rows[i]["ProductID"].ToString();
                    barcode.ProductName = dt.Rows[i]["ProductName"].ToString();
                    barcode.BarcodeNumber = dt.Rows[i]["Barcode"].ToString();
                    barcode.LotDate = dt.Rows[i]["LotDate"].ToString();
                    barcode.LotNumber = LotNumber;


                    barCodeDetails.Add(barcode);
                }
            }


            return barCodeDetails;
        }


        public List<StockStatus> StockInitialization(string BarcodeNumber, string StockAction, string UserID)
        {
            List<StockStatus> stockStat = new List<StockStatus>();
            StockStatus ss;

            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(Connection);
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_StockAction", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = UserID; // added by gautam
            cmd.Parameters.Add("@BarcodeNumber", SqlDbType.NVarChar).Value = Convert.ToString(BarcodeNumber);
            cmd.Parameters.Add("@StockAction", SqlDbType.Int).Value = Convert.ToInt32(StockAction);


            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                ss = new StockStatus();
                ss.BarcodeStatus = StockAction;
                ss.Message = Convert.ToString(dt.Rows[0]["Message"]);
                ss.BarcodeNumber = BarcodeNumber;
                ss.ActionCompletion = Convert.ToBoolean(dt.Rows[0]["ActionCompletion"]);

                stockStat.Add(ss);
            }
            else
            {
                ss = new StockStatus();
                ss.BarcodeStatus = string.Empty;
                ss.Message = "The Barcode is invalid or not yet generated. Please contact the admin for more details";
                ss.BarcodeNumber = BarcodeNumber;
                ss.ActionCompletion = false;
                stockStat.Add(ss);
            }

            return stockStat;
        }

        #region // cheheck for barcode status for POS

        public PosDetails PosInitialization(string BarcodeNumber)
        {
            PosDetails posDetails = new PosDetails();

            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_GetPosBarcodeStatusCheck", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@BarcodeNumber", SqlDbType.NVarChar).Value = Convert.ToString(BarcodeNumber);

            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();

            if (dt.Rows.Count > 0)
            {
                posDetails.ActiveState = true;
                posDetails.BarcodeNumber = dt.Rows[0]["Barcode"].ToString();
                posDetails.BarcodeStatus = dt.Rows[0]["BarcodeStatus"].ToString();
                posDetails.GST = Convert.ToInt32(dt.Rows[0]["GST"].ToString());
                posDetails.HSNCode = Convert.ToInt32(dt.Rows[0]["HSN"].ToString());
                posDetails.ProductName = dt.Rows[0]["ProductName"].ToString();
                posDetails.BillingName = dt.Rows[0]["BillingName"].ToString();
            }
            else
            {
                posDetails.ActiveState = false;
            }

            return posDetails;
        }

        #endregion

        #region // for bill details and bill summary enter through SP

        public List<PosDetails> BillDetails(int TotalQty, double GrossBillValue, double BillDiscount, double TotalGSTValue, double NetBillValue, DataTable dtBillDetails, double DiscountAmount, int TransactionType, double CashAmount, double CardAmount, int CardNumber, string UserID, bool PendingBill, double PendingAmount)
        {
            float TotalPaidAmount = (float)CashAmount + (float)CardAmount;


            List<PosDetails> posDetails = new List<PosDetails>();
            PosDetails bill;
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("stp_srv_PosGenerateBillDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = 1234;



            cmd.Parameters.Add("@TotalQty", SqlDbType.Int).Value = TotalQty;
            cmd.Parameters.Add("@GrossBillValue", SqlDbType.Float).Value = GrossBillValue;
            cmd.Parameters.Add("@BillDiscount", SqlDbType.Float).Value = BillDiscount;
            cmd.Parameters.Add("@TotalGSTValue", SqlDbType.Float).Value = TotalGSTValue;
            cmd.Parameters.Add("@NetBillValue", SqlDbType.Float).Value = NetBillValue;
            cmd.Parameters.Add("@DiscountAmount", SqlDbType.Float).Value = DiscountAmount;
            cmd.Parameters.Add("@TransactionType", SqlDbType.Int).Value = TransactionType;
            cmd.Parameters.Add("@CashAmount", SqlDbType.Float).Value = CashAmount;
            cmd.Parameters.Add("@CardAmount", SqlDbType.Float).Value = CardAmount;
            cmd.Parameters.Add("@CardNumber", SqlDbType.Int).Value = CardNumber;
            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = UserID;
            cmd.Parameters.Add("@PendingBill", SqlDbType.Bit).Value = PendingBill;
            cmd.Parameters.Add("@PendingAmount", SqlDbType.Float).Value = PendingAmount;
            cmd.Parameters.Add("@TotalPaidAmount", SqlDbType.Float).Value = TotalPaidAmount;

            SqlParameter param = cmd.Parameters.AddWithValue("@BillList", dtBillDetails);

            param.SqlDbType = SqlDbType.Structured;

            //  cmd.ExecuteNonQuery();

            da.SelectCommand = cmd;
            da.Fill(ds);
            con.Close();
            // PosDetails bill;
            if (ds.Tables[0].Rows.Count > 0)
            {


                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    bill = new PosDetails();
                    bill.ActiveState = true;
                    bill.BillNumber = ds.Tables[1].Rows[i]["BillNumber"].ToString();
                    bill.totalQty = Convert.ToInt32(ds.Tables[1].Rows[i]["TotalQty"]);
                    bill.GrossBillValue = (float)Convert.ToDouble(ds.Tables[1].Rows[i]["GrossBillValue"]);
                    bill.Discount = Convert.ToInt32(ds.Tables[1].Rows[i]["BillDiscount"]);
                    bill.GST = (float)Convert.ToDouble(ds.Tables[1].Rows[i]["TotalGSTValue"]);
                    bill.NetBillValue = Convert.ToInt32(ds.Tables[1].Rows[i]["NetBillValue"]);
                    bill.DiscountAmount = Convert.ToInt16(ds.Tables[1].Rows[i]["DiscountAmount"]);
                    bill.CGSTValue = (float)Convert.ToDouble(ds.Tables[1].Rows[i]["CGSTValue"]);
                    bill.SGSTValue = (float)Convert.ToDouble(ds.Tables[1].Rows[i]["SGSTValue"]);
                    bill.IGSTValue = (float)Convert.ToDouble(ds.Tables[1].Rows[i]["IGS" +
                        "TValue"]);
                    bill.ItemGSTValue = (float)Convert.ToDouble(ds.Tables[1].Rows[i]["ItemGSTValue"]);
                    bill.BillingName = ds.Tables[1].Rows[i]["ProductBillingName"].ToString();
                    bill.Cost = (float)Convert.ToDouble(ds.Tables[1].Rows[i]["TotalValue"]);
                    bill.Rate = (float)Convert.ToDouble(ds.Tables[1].Rows[i]["Rate"]);
                    bill.BarcodeNumber = ds.Tables[1].Rows[i]["Barcode"].ToString();
                    bill.GSTpercent = Convert.ToInt32(ds.Tables[1].Rows[i]["GSTPercent"]);
                    bill.HSNCode = Convert.ToInt32(ds.Tables[1].Rows[i]["HSN"]);
                    bill.BillDate = Convert.ToString(ds.Tables[1].Rows[i]["BillDate"]);
                    bill.BillPendingAmount = (float)Convert.ToDouble(ds.Tables[1].Rows[i]["BillRemainderValue"]);


                    posDetails.Add(bill);
                }

            }
            else
            {
                bill = new PosDetails();

                bill.ActiveState = false;
                posDetails.Add(bill);
            }

            return posDetails;
        }

        #endregion


        #region Pending Bill Clearance

        public List<PosDetails> ClearPendingBill(string BillNumber, float AmountPaid, float PendingAmount, string UserID)
        {
            List<PosDetails> posDetails = new List<PosDetails>();
            PosDetails bill;

            bool BillState = true;

            if (PendingAmount == 0)
            {
                BillState = false;
            }

            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("stp_srv_PosClearPendingBill", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BillNumber", SqlDbType.NVarChar).Value = BillNumber;
            cmd.Parameters.Add("@AmountPaid", SqlDbType.Float).Value = AmountPaid;
            cmd.Parameters.Add("@PendingAmount", SqlDbType.Float).Value = PendingAmount;
            cmd.Parameters.Add("@BillState", SqlDbType.Bit).Value = BillState;
            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = UserID;
            da.SelectCommand = cmd;
            da.Fill(ds);
            con.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    bill = new PosDetails();
                    bill.ActiveState = true;
                    bill.BillNumber = ds.Tables[0].Rows[i]["BillNumber"].ToString();
                    bill.totalQty = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalQty"]);
                    bill.GrossBillValue = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["GrossBillValue"]);
                    bill.Discount = Convert.ToInt32(ds.Tables[0].Rows[i]["BillDiscount"]);
                    bill.GST = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["TotalGSTValue"]);
                    bill.NetBillValue = Convert.ToInt32(ds.Tables[0].Rows[i]["NetBillValue"]);
                    bill.DiscountAmount = Convert.ToInt16(ds.Tables[0].Rows[i]["DiscountAmount"]);
                    bill.CGSTValue = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["CGSTValue"]);
                    bill.SGSTValue = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["SGSTValue"]);
                    bill.IGSTValue = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["IGSTValue"]);
                    bill.ItemGSTValue = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["ItemGSTValue"]);
                    bill.BillingName = ds.Tables[0].Rows[i]["ProductBillingName"].ToString();
                    bill.Cost = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["TotalValue"]);
                    bill.Rate = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["Rate"]);
                    bill.BarcodeNumber = ds.Tables[0].Rows[i]["Barcode"].ToString();
                    bill.GSTpercent = Convert.ToInt32(ds.Tables[0].Rows[i]["GSTPercent"]);
                    bill.HSNCode = Convert.ToInt32(ds.Tables[0].Rows[i]["HSN"]);
                    bill.BillDate = Convert.ToString(ds.Tables[0].Rows[i]["BillDate"]);
                    bill.BillPendingAmount = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["BillRemainderValue"]);

                    posDetails.Add(bill);
                }

            }
            else
            {
                bill = new PosDetails();

                bill.ActiveState = false;
                posDetails.Add(bill);
            }

            return posDetails;
        }

        #endregion

        #region ReprintBill

        public List<PosDetails> RePrintBill(string BillNumber)
        {
            List<PosDetails> posDetails = new List<PosDetails>();
            PosDetails bill;


            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("stp_srv_RePrintBill", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@BillNumber", SqlDbType.NVarChar).Value = BillNumber;

            da.SelectCommand = cmd;
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    bill = new PosDetails();
                    bill.ActiveState = true;
                    bill.BillNumber = ds.Tables[0].Rows[i]["BillNumber"].ToString();
                    bill.totalQty = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalQty"]);
                    bill.GrossBillValue = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["GrossBillValue"]);
                    bill.Discount = Convert.ToInt32(ds.Tables[0].Rows[i]["BillDiscount"]);
                    bill.GST = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["TotalGSTValue"]);
                    bill.NetBillValue = Convert.ToInt32(ds.Tables[0].Rows[i]["NetBillValue"]);
                    bill.DiscountAmount = Convert.ToInt16(ds.Tables[0].Rows[i]["DiscountAmount"]);
                    bill.CGSTValue = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["CGSTValue"]);
                    bill.SGSTValue = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["SGSTValue"]);
                    bill.IGSTValue = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["IGSTValue"]);
                    bill.ItemGSTValue = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["ItemGSTValue"]);
                    bill.BillingName = ds.Tables[0].Rows[i]["ProductBillingName"].ToString();
                    bill.Cost = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["TotalValue"]);
                    bill.Rate = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["Rate"]);
                    bill.BarcodeNumber = ds.Tables[0].Rows[i]["Barcode"].ToString();
                    bill.GSTpercent = Convert.ToInt32(ds.Tables[0].Rows[i]["GSTPercent"]);
                    bill.HSNCode = Convert.ToInt32(ds.Tables[0].Rows[i]["HSN"]);
                    bill.BillDate = Convert.ToString(ds.Tables[0].Rows[i]["BillDate"]);
                    bill.BillPendingAmount = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["BillRemainderValue"]);

                    posDetails.Add(bill);
                }

            }
            else
            {
                bill = new PosDetails();

                bill.ActiveState = false;
                posDetails.Add(bill);
            }

            return posDetails;
        }

        #endregion

        #region GetPending Bill

        public List<PosDetails> GetPendingBill()
        {
            List<PosDetails> posDetails = new List<PosDetails>();
            PosDetails bill;
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("stp_srv_SelectPendingBill", con);
            cmd.CommandType = CommandType.StoredProcedure;



            da.SelectCommand = cmd;
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    bill = new PosDetails();
                    bill.ActiveState = true;
                    bill.BillNumber = ds.Tables[0].Rows[i]["BillNumber"].ToString();
                    bill.totalQty = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalQty"]);
                    bill.GrossBillValue = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["GrossBillValue"]);
                    bill.Discount = Convert.ToInt32(ds.Tables[0].Rows[i]["BillDiscount"]);
                    bill.GST = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["TotalGSTValue"]);
                    bill.NetBillValue = Convert.ToInt32(ds.Tables[0].Rows[i]["NetBillValue"]);
                    bill.BillDate = Convert.ToString(ds.Tables[0].Rows[i]["BillDate"]);
                    bill.BillPendingAmount = (float)Convert.ToDouble(ds.Tables[0].Rows[i]["BillRemainderValue"]);

                    posDetails.Add(bill);
                }

            }
            else
            {
                bill = new PosDetails();

                bill.ActiveState = false;
                posDetails.Add(bill);
            }

            return posDetails;
        }

        #endregion

    }
}
