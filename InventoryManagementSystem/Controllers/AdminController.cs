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
    public class AdminController : ControllerBase
    {
        AdminDataAccessLayer adal = new AdminDataAccessLayer();

        #region UserProfile
        [HttpPost]
        [ActionName("NewUser")]
        [Route("[action]")]
        public ActionResult NewUser([FromBody] JObject postbody)
        {
            string FirstName = postbody["FirstName"].ToString();
            string MiddleName = postbody["MiddleName"].ToString();
            string LastName = postbody["LastName"].ToString();
            string UserID = postbody["UserID"].ToString();
            string Password = postbody["Password"].ToString();
            string UserRole = postbody["UserRole"].ToString();
            string Action = postbody["Action"].ToString();
            // newUD = adal.AddNewUser(FirstName, MiddleName, LastName, UserID, Password, UserRole, Action);
            var ANU=  adal.AddNewUser(FirstName, MiddleName, LastName, UserID, Password, UserRole, Action);
            return Ok(ANU);
        }
        [HttpPost]
        [ActionName("GetUserList")]
        [Route("[action]")]
        public ActionResult GetUserList([FromBody] JObject postbody)
        {
            List<UserDetails> readud = new List<UserDetails>();
            string Action = postbody["Action"].ToString();
            readud = adal.GetAllUser(Action);
            return Ok(readud);
        }
        [HttpPost]
        [ActionName("UpdateUser")]
        [Route("[action]")]
        public ActionResult UpdateUser([FromBody] JObject postbody)
        {
            int UID = Convert.ToInt32(postbody["UID"]);
            string FirstName = postbody["FirstName"].ToString();
            string MiddleName = postbody["MiddleName"].ToString();
            string LastName = postbody["LastName"].ToString();
            string UserID = postbody["UserID"].ToString();
            string UserRole = postbody["UserRole"].ToString();
            string Action = postbody["Action"].ToString();
            var UDU = adal.EditUser(UID, FirstName, MiddleName, LastName, UserID,/* Password,*/ UserRole, Action);
            return Ok(UDU);
        }
        [HttpPost]
        [ActionName("DeleteUser")]
        [Route("[action]")]
        public ActionResult DeleteUser([FromBody] JObject postbody)
        {            
            int UID = Convert.ToInt32(postbody["UID"]);
            string Action = postbody["Action"].ToString();
            var DU = adal.DeleteUserInfo(UID, Action);
            return Ok(DU);
        }

        #endregion

        #region Product

        [HttpPost]
        [ActionName("NewProduct")]
        [Route("[action]")]
        public ActionResult NewProduct([FromBody] JObject postbody)
        {
            string ProductName = postbody["ProductName"].ToString();
            string BillingName = postbody["BillingName"].ToString();
            string Type = postbody["Type"].ToString();
            int GST = Convert.ToInt32(postbody["GST"]);
            int HSNCode = Convert.ToInt32(postbody["HSNCode"]);
            string Action = postbody["Action"].ToString();
            var ANP = adal.AddNewProduct(ProductName, BillingName, Type, GST, HSNCode, Action);
            return Ok(ANP);
        }
        [HttpPost]
        [ActionName("UpdateProduct")]
        [Route("[action]")]
        public ActionResult UpdateProduct([FromBody] JObject postbody)
        {
            int ProductID = Convert.ToInt32(postbody["ProductID"]);
            string ProductName = postbody["ProductName"].ToString();
            string BillingName = postbody["BillingName"].ToString();
            string Type = postbody["Type"].ToString();
            int GST = Convert.ToInt32(postbody["GST"]);
            int HSNCode = Convert.ToInt32(postbody["HSNCode"]);
            string Action = postbody["Action"].ToString();
            var UP = adal.EditProduct(ProductID, ProductName, BillingName, Type, GST, HSNCode, Action);
            return Ok(UP);
        }
        [HttpPost]
        [ActionName("DeleteProduct")]
        [Route("[action]")]
        public ActionResult DeleteProduct([FromBody] JObject postbody)
        {          
            int ProductID = Convert.ToInt32(postbody["ProductID"]);
            string Action = postbody["Action"].ToString();
            var DP = adal.DeleteProductDetails(ProductID, Action);
            return Ok(DP);
        }
        [HttpPost]
        [ActionName("GetAllProductList")]
        [Route("[action]")]
        public ActionResult GetAllProductList([FromBody] JObject postbody)
        {
            List<ProductDetails> pd = new List<ProductDetails>();
            string Action = postbody["Action"].ToString();
            pd = adal.AllProductList(Action);
            return Ok(pd);
        }

        #endregion

        #region Vendor
        [HttpPost]
        [ActionName("NewVendor")]
        [Route("[action]")]
        public ActionResult NewVendor([FromBody] JObject postbody)
        {
            string VendorName = postbody["VendorName"].ToString();
            string VendorLocation = postbody["VendorLocation"].ToString();
            string Action = postbody["Action"].ToString();
            var NV = adal.AddNewVEndor(VendorName, VendorLocation, Action);
            return Ok(NV);
        }
        [HttpPost]
        [ActionName("GetVendorList")]
        [Route("[action]")]
        public ActionResult GetVendorList([FromBody] JObject postbody)
        {
            List<VendorDetails> vd = new List<VendorDetails>();
            string Action = postbody["Action"].ToString();
            vd = adal.VendorList(Action);
            return Ok(vd);
        }
        [HttpPost]
        [ActionName("UpdateVendor")]
        [Route("[action]")]
        public ActionResult UpdateVendor([FromBody] JObject postbody)
        {           
            int VendorID = Convert.ToInt32(postbody["VendorID"]);
            string VendorName = postbody["VendorName"].ToString();
            string VendorLocation = postbody["VendorLocation"].ToString();
            string Action = postbody["Action"].ToString();
            var UV = adal.EditVendor(VendorID, VendorName, VendorLocation, Action);
            return Ok(UV);
        }
        [HttpPost]
        [ActionName("DeleteVendor")]
        [Route("[action]")]
        public ActionResult DeleteVendor([FromBody] JObject postbody)
        {          
            int VendorID = Convert.ToInt32(postbody["VendorID"]);
            string Action = postbody["Action"].ToString();
            var DV = adal.DeleteVendorDetails(VendorID, Action);
            return Ok(DV);
        }

        #endregion

        #region Role
        [HttpPost]
        [ActionName("NewRoleMaster")]
        [Route("[action]")]
        public ActionResult NewRoleMaster([FromBody] JObject postbody)
        {         
            string RoleName = postbody["RoleName"].ToString();
            //  string CreatedBy = postbody["CreatedBy"].ToString();
            string Action = postbody["Action"].ToString();
            var  NRM = adal.AddNewRoleMaster(RoleName, Action  /*,CreatedBy*/);
            return Ok(NRM);
        }
        [HttpPost]
        [ActionName("GetAllRoleMaster")]
        [Route("[action]")]
        public ActionResult GetAllRoleMaster([FromBody] JObject postbody)
        {
            List<RoleDetails> listrd = new List<RoleDetails>();
            string Action = postbody["Action"].ToString();
            listrd = adal.GetRoleMaster(Action);
            return Ok(listrd);
        }
        [HttpPost]
        [ActionName("UpdataRoleMaster")]
        [Route("[action]")]
        public ActionResult UpdateRoleMaster([FromBody] JObject postbody)
        {         
            int RoleID = Convert.ToInt32(postbody["RoleID"]);
            string RoleName = postbody["RoleName"].ToString();
            string Action = postbody["Action"].ToString();
            //  string CreatedBy = postbody["CreatedBy"].ToString();
            var URM = adal.UpdateRole(RoleID, RoleName, Action);
            return Ok(URM);
        }
        [HttpPost]
        [ActionName("DeleteRoleMaster")]
        [Route("[action]")]
        public ActionResult DeleteRole([FromBody] JObject postbody)
        {           
            int RoleID = Convert.ToInt32(postbody["RoleID"]);
            string Action = postbody["Action"].ToString();
            var DR = adal.DeleteRole(RoleID, Action);
            return Ok(DR);
        }

        #endregion

        #region Status

        [HttpPost]
        [ActionName("NewStatusMaster")]
        [Route("[action]")]
        public ActionResult NewStatusMaster([FromBody] JObject postbody)
        {          
            string Action = postbody["Action"].ToString();
            string StatusName = postbody["StatusName"].ToString();
            var NSM = adal.AddNewStatus(StatusName, Action);
            return Ok(NSM);
        }
        [HttpPost]
        [ActionName("GetStatusMasterList")]
        [Route("[action]")]
        public ActionResult GetStatuemasterlist([FromBody] JObject postbody)
        {
            List<StatusDetails> sd = new List<StatusDetails>();
            string Action = postbody["Action"].ToString();
            sd = adal.GetStatuesMaster(Action);
            return Ok(sd);
        }
        [HttpPost]
        [ActionName("UpdateStatusMaster")]
        [Route("[action]")]
        public ActionResult UpdateStatusMaster([FromBody] JObject postbody)
        {
            int StatusID = Convert.ToInt32(postbody["StatusID"]);
            string StatusName = postbody["StatusName"].ToString();
            string Action = postbody["Action"].ToString();
            var USM = adal.UpdateStatues(StatusID, StatusName, Action);

            return Ok(USM);
        }
        [HttpPost]
        [ActionName("DeleteStatusMaster")]
        [Route("[action]")]
        public ActionResult DeleteStatusMaster([FromBody] JObject postbody)
        {

            int StatusID = Convert.ToInt32(postbody["StatusID"]);
            string Action = postbody["Action"].ToString();
            var DSM = adal.DeleteStatues(StatusID, Action);
            return Ok(DSM);
        }

        #endregion

        #region Location



        [HttpPost]
        [ActionName("NewLocation")]
        [Route("[action]")]
        public ActionResult NewLocation([FromBody] JObject postbody)
        {            
            string LocationName = postbody["LocationName"].ToString();
            string Action = postbody["Action"].ToString();
           var NL = adal.AddNewLocation(LocationName, Action);
            return Ok(NL);
        }
        [HttpPost]
        [ActionName("GetLocationMaster")]
        [Route("[action]")]
        public ActionResult GetLocationMaster([FromBody] JObject postbody)
        {
            List<LocationDetails> listld = new List<LocationDetails>();
            string Action = postbody["Action"].ToString();
            listld = adal.GetLocationList(Action);
            return Ok(listld);
        }
        [HttpPost]
        [ActionName("UpdateLocation")]
        [Route("[action]")]
        public ActionResult UpdateLocation([FromBody] JObject postbody)
        {        
            int LocationID = Convert.ToInt32(postbody["LocationID"]);
            string LocationName = postbody["LocationName"].ToString();
            string Action = postbody["Action"].ToString();
            var UL = adal.UpdateLocationMaster(LocationID, LocationName, Action);
            return Ok(UL);
        }
        [HttpPost]
        [ActionName("DeleteLocation")]
        [Route("[action]")]
        public ActionResult DeleteLocation([FromBody] JObject postbody)
        {          
            int LocationID = Convert.ToInt32(postbody["LocationID"]);
            string Action = postbody["Action"].ToString();
            var DL = adal.DeleteLocationMaster(LocationID, Action);        
            return Ok(DL);
        }

        #endregion

       
    }
}