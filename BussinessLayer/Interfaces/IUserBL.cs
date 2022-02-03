using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface IUserBL
    {
        public bool UserRegistration(RegistrationModel model);
        public Loginmodel Login(Loginmodel login);
        public string GenerateToken(string emailId);
        public string ForgotPassword(string emailId);
        public ResetPasswordModel ResetPassword(ResetPasswordModel resetPassword);
    }
}
