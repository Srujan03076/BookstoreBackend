using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface ICartBL
    {
        public bool AddtoCart(CartModel model);
        public bool UpdateCart(int cartId, int quantity);
        public bool DeleteCart(int cartId);
        List<CartModel> GetCart(int userId);
    }
}
