using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IOrderRL
    {
        public bool AddOrder(OrderModel model);
        OrderModel UpdateOrder(OrderModel model);
        List<OrderModel> GetOrder(int userId);
    }
}
