using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {

        [HttpPost]
        [ActionName("GenerateBarcodeByLot")]
        [Route("[action]")]
        public ActionResult GenerateBarcodeByLot([FromBody] JObject postBody)
        {

            //UserDetails ud = new UserDetails();
            string BillingName = postBody["BillingName"].ToString();
            string Quantity = postBody["Quantity"].ToString();

            // ud = udal.GetUserDetails(UserID, Password);


            //if (UserID == "2413" && Password == "@abcd1234")
            //{

            //    ud = udal.GetUserDetails();
            //}


            //return Ok(ud);

            return Ok();
        }



        [HttpPost]
        [ActionName("UpdateStockForStore")]
        [Route("[action]")]
        public ActionResult UpdateStockForStore([FromBody] JObject postBody)
        {

            //UserDetails ud = new UserDetails();
            string BillingName = postBody["BillingName"].ToString();
            string Quantity = postBody["Quantity"].ToString();

            
            return Ok();
        }


        [HttpPost]
        [ActionName("UpdateStockForSale")]
        [Route("[action]")]
        public ActionResult UpdateStockForSale([FromBody] JObject postBody)
        {

            //UserDetails ud = new UserDetails();
            string BillingName = postBody["BillingName"].ToString();
            string Quantity = postBody["Quantity"].ToString();


            return Ok();
        }

    }
}