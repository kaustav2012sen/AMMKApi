using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    public class AdminDataAccessLayer
    {
        string Connection = Startup.ConnectionString;

        #region User

        public string AddNewUser(string FirstName, string MiddleName, string LastName, string UserName, string Password, string RoleID, string Action)
        {
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_UserCRUD", con);
            // Why do you write the name of sp as get new user. it should be stpsrvaddnewuser
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = FirstName;
            cmd.Parameters.Add("@MiddleName", SqlDbType.NVarChar).Value = MiddleName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = LastName;
            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName;
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Password;
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = Convert.ToInt32(RoleID);
         
          //   da.SelectCommand = cmd;
          //  da.Fill(dt);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
               
             
                   return "New User Details  Not  Insert";
            }
            else 
            {
              
              
                return "Successfully New User Details Insert";
            }
        }
        public List<UserDetails> GetAllUser(string Action)
        {
            List<UserDetails> ud = new List<UserDetails>();
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_UserCRUD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();

            UserDetails alluser;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                alluser = new UserDetails();
                alluser.FirstName = dt.Rows[i]["FirstName"].ToString();
                alluser.MiddleName = dt.Rows[i]["MiddleName"].ToString();
                alluser.LastName = dt.Rows[i]["LastName"].ToString();
                alluser.UserID = dt.Rows[i]["UserName"].ToString();
                alluser.UserRole = dt.Rows[i]["RoleID"].ToString();

                ud.Add(alluser);
            }



            return ud;
        }
        public string  EditUser(int UID, string FirstName, string MiddleName, string LastName, string UserID,/* string Password,*/ string UserRole, string Action)
        {
         
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_UserCRUD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UID", SqlDbType.Int).Value = Convert.ToInt32(UID);
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = FirstName;
            cmd.Parameters.Add("@MiddleName", SqlDbType.NVarChar).Value = MiddleName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = LastName;
            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserID;
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;
            // cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Password;
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = Convert.ToInt32(UserRole);

            //da.SelectCommand = cmd;
            //da.Fill(dt);           
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return "User Details  Not Update";
            }
            else
            {
                return "Successfully  User Details Update";
            }

        }
        public string DeleteUserInfo(int UID, string Action)
        {
          
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_UserCRUD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UID", SqlDbType.Int).Value = Convert.ToInt32(UID);
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;
            da.SelectCommand = cmd;
            da.Fill(dt);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return "User Infomation  Not Delete";
            }
            else
            {
                return "Successfully  User Infomation Delete";
            }
        }

        #endregion

        #region Product

        public string AddNewProduct(string Pname, string billname, string type, int gst, int hsn, string Action)
        {
          
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_ProductCRUD", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Pname", SqlDbType.NVarChar).Value = (Pname);
            cmd.Parameters.Add("@Billname", SqlDbType.NVarChar).Value = (billname);
            cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = (type);
            cmd.Parameters.Add("@Gst", SqlDbType.Float).Value = (gst);
            cmd.Parameters.Add("@Hsn", SqlDbType.Float).Value = (hsn);
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;
            //da.SelectCommand = cmd;
            //da.Fill(dt);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return "New Product not Insert";
            }
            else
            {
                return "Successfully New Product Insert ";
            }
        }
        public string  EditProduct(int ProductID, string ProductName, string BillingName, string Type, int GST, int HSNCode, string Action)
        {
          
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_ProductCRUD", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = (ProductID);
            cmd.Parameters.Add("@Pname", SqlDbType.NVarChar).Value = (ProductName);
            cmd.Parameters.Add("@Billname", SqlDbType.NVarChar).Value = (BillingName);
            cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = (Type);
            cmd.Parameters.Add("@Gst", SqlDbType.Float).Value = (GST);
            cmd.Parameters.Add("@Hsn", SqlDbType.Float).Value = (HSNCode);
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;
            //da.SelectCommand = cmd;
            //da.Fill(dt);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return "Product Not Update";
            }
            else
            {
                return "Successfully Product Update";
            }

        }

        public string DeleteProductDetails(int PID, string Action)
        {
           
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_ProductCRUD", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = (PID);
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;
            //da.SelectCommand = cmd;
            //da.Fill(dt);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return "Product Not Delete";
            }
            else
            {
                return "Successfully  Product Delete";
            }

        }
        public List<ProductDetails> AllProductList(string Action)
        {
            List<ProductDetails> pd = new List<ProductDetails>();
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_ProductCRUD", con);
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;
            cmd.CommandType = CommandType.StoredProcedure;

            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            ProductDetails allPro;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                allPro = new ProductDetails();
                allPro.ProductID = Convert.ToInt32(dt.Rows[i]["ProductID"].ToString());
                allPro.ProductName = dt.Rows[i]["ProductName"].ToString();
                allPro.BillingName = dt.Rows[i]["BillingName"].ToString();
                allPro.Type = dt.Rows[0]["Type"].ToString();
                allPro.GST = Convert.ToInt32(dt.Rows[i]["GST"].ToString());
                allPro.HSNCode = Convert.ToInt32(dt.Rows[i]["HSN"].ToString());

                pd.Add(allPro);
            }


            return pd;
        }


        #endregion

        #region Vendor
        public string AddNewVEndor(string vname, string vlocation, string Action)
        {
          
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_VendorCRUD", con);

            cmd.Parameters.Add("@vname", SqlDbType.NVarChar).Value = vname;
            cmd.Parameters.Add("@vlocation", SqlDbType.NVarChar).Value = Convert.ToString(vlocation);
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;
            cmd.CommandType = CommandType.StoredProcedure;

            da.SelectCommand = cmd;
            da.Fill(dt);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return "Vendor Not Added";
            }
            else
            {
                return "New Vendor Added Successfully";
            }

        }
        public List<VendorDetails> VendorList(string Action)
        {
            List<VendorDetails> listvd = new List<VendorDetails>();
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_VendorCRUD", con);
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;

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
                vd.VendorLocation = dt.Rows[i]["LocationID"].ToString();


                listvd.Add(vd);
            }
            return listvd;

        }
        public string EditVendor(int vid, string vname, string vlocation, string Action)
        {
            VendorDetails vd = new VendorDetails();
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_VendorCRUD", con);
            cmd.Parameters.Add("@Vid", SqlDbType.Int).Value = Convert.ToInt32(vid);
            cmd.Parameters.Add("@vname", SqlDbType.NVarChar).Value = vname;
            cmd.Parameters.Add("@vlocation", SqlDbType.NVarChar).Value = Convert.ToString(vlocation);
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;
            cmd.CommandType = CommandType.StoredProcedure;

            //da.SelectCommand = cmd;
            //da.Fill(dt);

            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return "Vendor Not Update";
            }
            else
            {
                return "Vendor Update Successfully";
            }



        }
        public string DeleteVendorDetails(int vid, string Action)
        {
           
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_VendorCRUD", con);
            cmd.Parameters.Add("@Vid", SqlDbType.Int).Value = Convert.ToInt32(vid);
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;
            cmd.CommandType = CommandType.StoredProcedure;

            da.SelectCommand = cmd;
            da.Fill(dt);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return "Vendor Not Delete";
            }
            else
            {
                return "Vendor Delete Successfully";
            }

        }


        #endregion

        #region Role
        public string  AddNewRoleMaster(string Rname, string Action/*CreatedBy*/)
        {
          
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_RoleCRUD", con);

            cmd.Parameters.Add("@Rname", SqlDbType.NVarChar).Value = Rname;
            //  cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = CreatedBy;
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;
            cmd.CommandType = CommandType.StoredProcedure;

            //da.SelectCommand = cmd;
            //da.Fill(dt);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return "New Role Not Added";
            }
            else
            {
                return "New Role Added Successfully";
            }

        }

        public List<RoleDetails> GetRoleMaster(string Action)
        {
            List<RoleDetails> rd = new List<RoleDetails>();
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_RoleCRUD", con);
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            RoleDetails rdlist;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rdlist = new RoleDetails();
                rdlist.RoleName = dt.Rows[i]["RoleName"].ToString();
                rdlist.CreatedBy = dt.Rows[i]["CreatedBy"].ToString();

                rd.Add(rdlist);
            }
            return rd;
        }
        public string  UpdateRole(int rid, string rname, string Action)
        {
           
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_RoleCRUD", con);
            cmd.Parameters.Add("@rid", SqlDbType.Int).Value = rid;
            cmd.Parameters.Add("@Rname", SqlDbType.NVarChar).Value = rname;
            //  cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = CreatedBy;
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;
            cmd.CommandType = CommandType.StoredProcedure;

            //da.SelectCommand = cmd;          
            //da.Fill(dt);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return "Role Not Update";
            }
            else
            {
                return "Role Update Successfully";
            }

        }
        public string  DeleteRole(int rid, string Action)
        {
          
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_RoleCRUD", con);
            cmd.Parameters.Add("@rid", SqlDbType.Int).Value = rid;
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;

            cmd.CommandType = CommandType.StoredProcedure;

            //da.SelectCommand = cmd;
            //da.Fill(dt);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return "Role Not Delete";
            }
            else
            {
                return "Role Delete Successfully";
            }

        }

        #endregion

        #region Status
        public string  AddNewStatus(string sname, string Action)
        {
           
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_StatusCRUD", con);
            cmd.Parameters.Add("@sname", SqlDbType.NVarChar).Value = sname;
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;

            cmd.CommandType = CommandType.StoredProcedure;

            //da.SelectCommand = cmd;
            //da.Fill(dt);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return "New status Not Added";
            }
            else
            {
                return "New Status Added Successfully";
            }


        }
        public List<StatusDetails> GetStatuesMaster(string Action)
        {
            List<StatusDetails> listsd = new List<StatusDetails>();

            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_StatusCRUD", con);
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;

            cmd.CommandType = CommandType.StoredProcedure;

            //da.SelectCommand = cmd;
            //da.Fill(dt);
            con.Close();
            StatusDetails sd;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sd = new StatusDetails();
                sd.StatusName = dt.Rows[i]["StatusName"].ToString();
                listsd.Add(sd);
            }
            return listsd;
        }
        public string UpdateStatues(int sid, string sname, string Action)
        {
          
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_StatusCRUD", con);
            cmd.Parameters.Add("@sid", SqlDbType.NVarChar).Value = sid;
            cmd.Parameters.Add("@sname", SqlDbType.NVarChar).Value = sname;
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;
            cmd.CommandType = CommandType.StoredProcedure;

            //da.SelectCommand = cmd;
            //da.Fill(dt);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return "Status Not Update";
            }
            else
            {
                return "Status Update Successfully";
            }
        }
        public string DeleteStatues(int sid, string Action)
        {
          
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_StatusCRUD", con);
            cmd.Parameters.Add("@sid", SqlDbType.NVarChar).Value = sid;
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;
            cmd.CommandType = CommandType.StoredProcedure;

            //da.SelectCommand = cmd;
            //da.Fill(dt);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return "Status Not Delete";
            }
            else
            {
                return "Status Delete Successfully";
            }

        }

        #endregion

        #region Location
        public string AddNewLocation(string lname, string Action)
        {
         
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_LocationCRUD", con);
            cmd.Parameters.Add("@lname", SqlDbType.NVarChar).Value = lname;
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;
            cmd.CommandType = CommandType.StoredProcedure;

            //da.SelectCommand = cmd;
            //da.Fill(dt);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return "New Location Not Added";
            }
            else
            {
                return "New  Location Successfully Added";
            }

        }
        public List<LocationDetails> GetLocationList(string Action)
        {
            List<LocationDetails> listld = new List<LocationDetails>();
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_LocationCRUD", con);
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;

            cmd.CommandType = CommandType.StoredProcedure;

            da.SelectCommand = cmd;

            da.Fill(dt);

            con.Close();
            LocationDetails ld;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ld = new LocationDetails();
                ld.LocationName = dt.Rows[i]["LocationName"].ToString();
                listld.Add(ld);


            }

            return listld;
        }
        public string UpdateLocationMaster(int lid, string lname, string Action)
        {
          
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_LocationCRUD", con);
            cmd.Parameters.Add("@lid", SqlDbType.Int).Value = lid;
            cmd.Parameters.Add("@lname", SqlDbType.NVarChar).Value = lname;
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;
            cmd.CommandType = CommandType.StoredProcedure;

            //da.SelectCommand = cmd;
            //da.Fill(dt);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return "Location Not Update";
            }
            else
            {
                return "Location Update Successfully";
            }

        }
        public string DeleteLocationMaster(int lid, string Action)
        {
           
            SqlConnection con = new SqlConnection(Connection);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("stp_srv_LocationCRUD", con);
            cmd.Parameters.Add("@lid", SqlDbType.Int).Value = lid;
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Action;
            cmd.CommandType = CommandType.StoredProcedure;

            //da.SelectCommand = cmd;
            //da.Fill(dt);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return "Location Not Delete";
            }
            else
            {
                return "Location Update Successfully";
            }

        }

        #endregion
    }
}
