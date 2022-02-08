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
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL orderBL;
        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }
        [HttpPost]
        public IActionResult AddOrder(OrderModel model)
        {
            try
            {
                var result = this.orderBL.AddOrder(model);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message ="Order Added Successfully !" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to add Order" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, message = e.Message, InnerException = e.InnerException });
            }
        }
        [HttpPut]
        public IActionResult UpdateOrder(OrderModel model)
        {
            try
            {
                OrderModel result = this.orderBL.UpdateOrder(model);
                if (result != null)
                {

                    return this.Ok(new ResponseModel<OrderModel>() { Status = true, Message = "Updation Successful!", Data = result });
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
        [Route("{UserId}/GetOrder")]
        public IActionResult GetOrder(int UserId)
        {
            try
            {
                var result = this.orderBL.GetOrder(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Order details retrieved successfully", Data = result });

                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Order details retrieval failed" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, message = e.Message, InnerException = e.InnerException });
            }
        }

    }
}
    

