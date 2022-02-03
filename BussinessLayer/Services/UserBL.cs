using BussinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;

        }

        public string ForgotPassword(string emailId)
        {
            try
            {
                return this.userRL.ForgotPassword(emailId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string GenerateToken(string emailId)
        {

            try
            {
                return this.userRL.GenerateToken(emailId);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public Loginmodel Login(Loginmodel login)
        {
            try
            {
                return this.userRL.Login(login);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public ResetPasswordModel ResetPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                return this.userRL.ResetPassword(resetPassword);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public bool UserRegistration(RegistrationModel model)
        {
            try
            {
                return this.userRL.UserRegistration(model);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}

        



        
    
