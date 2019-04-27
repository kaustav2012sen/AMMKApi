using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        ProductDetailAccessLayer pdac = new ProductDetailAccessLayer();

        [HttpPost]
        [ActionName("GetProductList")]
        [Route("[action]")]
        public ActionResult GetProductList()
        {

            List<ProductDetails> pd = new List<ProductDetails>();

            pd = pdac.GetProductList();      


            return Ok(pd);
        }
    }
}