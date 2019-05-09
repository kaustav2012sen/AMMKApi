using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    public class VendorDetailAccessLayer
    {
        string Connection = Startup.ConnectionString;


        public List<VendorDetails> GetVendorList()
        {
            List<VendorDetails> vendorDetails = new List<VendorDetails>();

            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_GetUniqueVendorDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();

            VendorDetails vd;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                vd = new VendorDetails();

                vd.VendorID = Convert.ToInt32(dt.Rows[i]["VendorID"].ToString());
                vd.VendorName = dt.Rows[i]["VendorName"].ToString();
                vd.VendorLocation = dt.Rows[i]["VendorLocation"].ToString();
                

                vendorDetails.Add(vd);
            }



            return vendorDetails;
        }
    }
}
