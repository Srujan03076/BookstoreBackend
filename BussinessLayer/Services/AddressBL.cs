using BussinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class AddressBL:IAddressBL
    {
        IAddressRL addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;

        }

        public bool AddAddress(AddressModel model)
        {
            try
            {
                return this.addressRL.AddAddress(model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<AddressModel> GetAddress(int userId)
        {
            try
            {
                return this.addressRL.GetAddress(userId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public AddressModel UpdateAddress(AddressModel model)
        {
            try
            {
                return this.addressRL.UpdateAddress(model);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
