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
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL feedbackBL;
        public FeedbackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }
        [HttpPost]
        public IActionResult AddFeedback(FeedbackModel model)
        {
            try
            {
                var result = this.feedbackBL.AddFeedback(model);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Feedback Added Successfully !" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Failed to add feedback" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, message = e.Message, InnerException = e.InnerException });
            }
        }
        [HttpGet]
        [Route("{UserId}/GetFeedback")]
        public IActionResult GetFeedback(int UserId)
        {
            try
            {
                var result = this.feedbackBL.GetFeedback(UserId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Feedback details retrieved successfully", Data = result });

                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Feedback details retrieval failed" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, message = e.Message, InnerException = e.InnerException });
            }
        }
    }
}


    

