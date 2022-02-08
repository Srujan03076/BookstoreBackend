using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface IAddressBL
    {
        public bool AddAddress(AddressModel model);
        AddressModel UpdateAddress(AddressModel model);
        List<AddressModel> GetAddress(int userId);
    }
}
