using BussinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }
        [HttpPost]
        public IActionResult AddtoCart(CartModel model)
        {
            try
            {
                var result = this.cartBL.AddtoCart(model);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Book is added to cart" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Adding to bag failed ! try again" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, message = e.Message, InnerException = e.InnerException });
            }
        }

        [HttpPut]
        [Route("{CartId}/UpdateCart")]
        public IActionResult UpdateCart(int CartId, int Quantity)
        {
            try
            {
                var result = this.cartBL.UpdateCart(CartId, Quantity);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Cart Updated" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Updation Failed" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, message = e.Message, InnerException = e.InnerException });
            }
        }
        [HttpDelete]
        [Route("{CartId}/DeleteCart")]
        public IActionResult DeleteCart(int CartId)
        {
            try
            {
                var result = this.cartBL.DeleteCart(CartId);
                if (result == true)
                {

                    return this.Ok(new { Success = true, Message = "Deleted Cart Successfully !" });
                }
                else
                {

                    return this.BadRequest(new { Success = false, Message = "Failed to delete cart" });
                }
            }
            catch (Exception e)
            {

                return this.BadRequest(new { Status = false, message = e.Message, InnerException = e.InnerException });

            }
        }
        [HttpGet]
        [Route("{UserId}/GetCart")]
        public IActionResult GetCart(int UserId)
        {
            try
            {
                var result = this.cartBL.GetCart(UserId);
                if (result != null)
                {
                    return this.Ok(new{ Status = true, Message = "Cart details retrieved successfully", Data = result });

                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Cart details retrieval failed" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, message = e.Message, InnerException = e.InnerException });
            }
        }
    }
}


        


       
    

