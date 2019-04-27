using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        UserDetailAccessLayer udal = new UserDetailAccessLayer();


        [HttpGet]
        public ActionResult<IEnumerable<string>> Index()
        {
            return new string[] { "Welcome to Adi Mohini Mohan Kanjilal" };
        }





        [HttpPost]
        [ActionName("Post01")]
        [Route("[action]")]
        public ActionResult Post01([FromBody] JObject postBody)
        {

            UserDetails ud = new UserDetails();
            string UserID = postBody["UserID"].ToString();
            string Password = postBody["Password"].ToString();
           

            if (UserID == "2413" && Password == "@abcd1234")
            {

                ud = udal.GetUserDetails();
            }


            return Ok(ud);
        }

        [HttpPost]
        [ActionName("LoginAuthentication")]
        [Route("[action]")]
        public ActionResult LoginAuthentication([FromBody] JObject postBody)
        {

            UserDetails ud = new UserDetails();
            string UserID = postBody["UserID"].ToString();
            string Password = postBody["Password"].ToString();

            ud=udal.GetUserDetails(UserID, Password);


            //if (UserID == "2413" && Password == "@abcd1234")
            //{

            //    ud = udal.GetUserDetails();
            //}


            return Ok(ud);
        }


    }
}