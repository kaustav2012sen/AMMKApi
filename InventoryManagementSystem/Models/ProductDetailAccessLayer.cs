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
    }
}
