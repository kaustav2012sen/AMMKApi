using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    public class UserDetailAccessLayer
    {
        
        string Connection= Startup.ConnectionString;
        


        public UserDetails GetUserDetails()
        {
            try
            {

                

                UserDetails ud = new UserDetails();
                ud.UserID = "2413";
                ud.FirstName = "DevAdmin";
                ud.MiddleName = "DevAdmin";
                ud.LastName = "DevAdmin";
                ud.UserRole = "ADMIN";

                return ud;

            }
            catch
            {
                throw;
            }
        }

        public UserDetails GetUserDetails(string UserID,string Password)
        {
            UserDetails userDetails = new UserDetails();
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_GetUniqueUserDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID ", SqlDbType.NVarChar).Value =UserID;
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Password;


            //cmd.Parameters.Add("@MenuTag", SqlDbType.BigInt).Value = 0;

            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();


            userDetails.UserID = dt.Rows[0]["UserName"].ToString();
            userDetails.FirstName = dt.Rows[0]["FirstName"].ToString();
            userDetails.MiddleName = dt.Rows[0]["MiddleName"].ToString();
            userDetails.LastName = dt.Rows[0]["LastName"].ToString();
            userDetails.UserRole = dt.Rows[0]["RoleName"].ToString();



            return userDetails;
        }
    }
}
