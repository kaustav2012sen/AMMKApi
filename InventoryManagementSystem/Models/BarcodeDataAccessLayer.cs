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
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID; //Added by Goutam Giri
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
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID; // added by gautam
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


    }
}
