using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public bool UserRegistration(RegistrationModel model);
        public Loginmodel Login(Loginmodel login);

    }
}
