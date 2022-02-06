using BussinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class CartBL: ICartBL
    {
        ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;

        }

        public bool AddtoCart(CartModel model)
        {
            try
            {
                return this.cartRL.AddtoCart(model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool DeleteCart(int cartId)
        {
            try
            {
                return this.cartRL.DeleteCart(cartId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<CartModel> GetCart(int userId)
        {
            try
            {
                return this.cartRL.GetCart(userId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool UpdateCart(int cartId, int quantity)
        {
            try
            {
                return this.cartRL.UpdateCart(cartId, quantity);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
