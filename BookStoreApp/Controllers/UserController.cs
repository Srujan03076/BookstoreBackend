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
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost]
        public IActionResult UserRegistration(RegistrationModel model)
        {
            try
            {
                if (this.userBL.UserRegistration(model))
                {
                    return this.Ok(new { Success = true, message = "Registration Successful" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Registration unsuccessful" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.Message, InnerException = e.InnerException });
            }
        }
        [HttpPost]
        [Route("api/login")]
        public IActionResult Login(Loginmodel login)
        {
            try
            {
                string token = this.userBL.GenerateToken(login.EmailId);
                var result = this.userBL.Login(login);
                if (result.EmailId == null)
                {
                    return this.BadRequest(new { Success = false, message = "Email or Password not Found" });
                }
                return this.Ok(new { Success = true, message = "Login Successfuul", data = result, token=token });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, message = e.Message, InnerException = e.InnerException }); ;
            }

        }
        [HttpPost]
        [Route("api/forgotpassword")]
        public IActionResult ForgotPassword(string EmailId)
        {
            try
            {
                 string result = this.userBL.ForgotPassword(EmailId);
                if (result.Equals("Email is sent successfully"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result, });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, message = e.Message, InnerException = e.InnerException });
            }
        }
        [HttpPut]
        [Route("api/resetpassword")]
        public IActionResult ResetPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                ResetPasswordModel result = this.userBL.ResetPassword(resetPassword);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<ResetPasswordModel>() { Status = true, Message = "Password Reset Successfully",Data=result});

                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Password Reset Failed" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = e.Message });
            }

        }
    }
}

        
    

