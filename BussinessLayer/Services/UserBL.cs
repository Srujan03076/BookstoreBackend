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


        
    
