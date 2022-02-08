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
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL addressBL;
        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }
        [HttpPost]
        public IActionResult AddAddress(AddressModel model)
        {
            try
            {
                var result = this.addressBL.AddAddress(model);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Address added Successfully !" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to add Address" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, message = e.Message, InnerException = e.InnerException });
            }
        }
        [HttpPut]
        public IActionResult UpdateAddress(AddressModel model)
        {
            try
            {
                AddressModel result = this.addressBL.UpdateAddress(model);
                if (result != null)
                {

                    return this.Ok(new ResponseModel<AddressModel>() { Status = true, Message = "Updation Successful!", Data = result });
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
        [HttpGet]
        [Route("{UserId}/GetAddress")]
        public IActionResult GetAddress(int UserId)
        {
            try
            {
                var result = this.addressBL.GetAddress(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Address details retrieved successfully", Data = result });

                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Address details retrieval failed" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, message = e.Message, InnerException = e.InnerException });
            }
        }
    }

    

    
}
