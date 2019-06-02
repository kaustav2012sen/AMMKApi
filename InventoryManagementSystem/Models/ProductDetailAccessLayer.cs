using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    public class ProductDetailAccessLayer
    {
        string Connection = Startup.ConnectionString;

        public List<ProductDetails> GetProductList()
        {
            List<ProductDetails> productDetails = new List<ProductDetails>();

            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_GetUniqueProductDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
           
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();

            ProductDetails pd;

            for (int i=0;i<dt.Rows.Count;i++)
            {
                 pd = new ProductDetails();

                pd.ProductID = Convert.ToInt32(dt.Rows[i]["ProductID"].ToString());
                pd.ProductName = dt.Rows[i]["ProductName"].ToString();
                pd.BillingName = dt.Rows[i]["BillingName"].ToString();
                pd.Type = dt.Rows[0]["Type"].ToString();
                pd.GST = Convert.ToInt32(dt.Rows[i]["GST"].ToString());
                pd.HSNCode = Convert.ToInt32(dt.Rows[i]["HSN"].ToString());

                productDetails.Add(pd);
            }

            

            return productDetails;
        }


        public InventoryModule GetInventoryModule()
        {
            InventoryModule inventoryModules = new InventoryModule();
            List<ProductDetails> productDetails = new List<ProductDetails>();
            List<VendorDetails> vendorDetails = new List<VendorDetails>();

            DataSet ds = new DataSet();

            //DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_GetUniqueVendorProductDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            da.SelectCommand = cmd;
            da.Fill(ds);
            con.Close();


            if(ds.Tables[0].Rows.Count>0)
            {
                ProductDetails pd;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    pd = new ProductDetails();

                    pd.ProductID = Convert.ToInt32(ds.Tables[0].Rows[i]["ProductID"].ToString());
                    pd.ProductName = ds.Tables[0].Rows[i]["ProductName"].ToString();
                    pd.BillingName = ds.Tables[0].Rows[i]["BillingName"].ToString();
                    pd.Type = ds.Tables[0].Rows[0]["Type"].ToString();
                    pd.GST = Convert.ToInt32(ds.Tables[0].Rows[i]["GST"].ToString());
                    pd.HSNCode = Convert.ToInt32(ds.Tables[0].Rows[i]["HSN"].ToString());

                    productDetails.Add(pd);
                }
            }

            if(ds.Tables[1].Rows.Count>0)
            {
                VendorDetails vd;

                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    vd = new VendorDetails();

                    vd.VendorID = Convert.ToInt32(ds.Tables[1].Rows[i]["VendorID"].ToString());
                    vd.VendorName = ds.Tables[1].Rows[i]["VendorName"].ToString();
                    vd.VendorLocation = ds.Tables[1].Rows[i]["LocationName"].ToString();


                    vendorDetails.Add(vd);
                }
            }

            inventoryModules.productDetails = productDetails;
            inventoryModules.vendorDetails = vendorDetails;
            return inventoryModules;
        }

        public List<Barcode> ProductLotBarcodeNumbe(int ProductID, int LotNumber)
        {
            List<Barcode> productlotbarcode = new List<Barcode>();
            Barcode barcodes;
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_GetProductLotIDBarcodeDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = Convert.ToInt32(ProductID);
            cmd.Parameters.Add("@LotNumber", SqlDbType.Int).Value = Convert.ToInt32(LotNumber);
            cmd.ExecuteNonQuery();

            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            for (int i = 0; i < dt.Rows.Count; i++)

            {
                barcodes = new Barcode();

                barcodes.BarcodeNumber = dt.Rows[i]["Barcode"].ToString();
                barcodes.LotNumber = Convert.ToString(LotNumber);

                productlotbarcode.Add(barcodes);



            }

            return productlotbarcode;



        }

        public List<ProductLotDetails> ProductLotNumberInfo(int ProductID)
        {

            List<ProductLotDetails> ProductLotInfo = new List<ProductLotDetails>();
            ProductLotDetails pld;


            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stv_srv_GetProductLotIdDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = Convert.ToInt32(ProductID);
            cmd.ExecuteNonQuery();

            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();

            for (int i = 0; i < dt.Rows.Count; i++)

            {
                pld = new ProductLotDetails();

                pld.LotNumber = Convert.ToInt32(dt.Rows[i]["LotNumber"].ToString());

                ProductLotInfo.Add(pld);



            }

            return ProductLotInfo;
        }
    }
}
