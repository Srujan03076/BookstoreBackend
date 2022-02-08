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
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistBL wishlistBL;
        public WishlistController(IWishlistBL wishlistBL)
        {
            this.wishlistBL = wishlistBL;
        }
        [HttpPost]
        public IActionResult AddToWishList(WishlistModel model)
        {
            try
            {
                var result = this.wishlistBL.AddToWishList(model);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Added To wish list Successfully !" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to add to wish list" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status =false, message = e.Message, InnerException = e.InnerException });
            }
        }
        [HttpDelete]
        [Route("{wishlistId}/DeleteWishlist")]
        public IActionResult DeleteWishlist(int wishlistId)
        {
            try
            {
                var result = this.wishlistBL.DeleteWishlist(wishlistId);
                if (result == true)
                {

                    return this.Ok(new { Success = true, Message = "Removed wishlist item Successfully !" });
                }
                else
                {

                    return this.BadRequest(new { Success = false, Message = "Failed to Remove wishlist item" });
                }
            }
            catch (Exception e)
            {

                return this.BadRequest(new { Status = false, message = e.Message, InnerException = e.InnerException });

            }
        }
        [HttpGet]
        [Route("{UserId}/GetWishlist")]
        public IActionResult GetWishlist(int UserId)
        {
            try
            {
                var result = this.wishlistBL.GetWishlist(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Wishlist details retrieved successfully", Data = result });

                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Wishlist details retrieval failed" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, message = e.Message, InnerException = e.InnerException });
            }
        }
    }
}
        
      


    

