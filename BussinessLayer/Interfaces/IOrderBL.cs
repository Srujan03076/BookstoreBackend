using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface IOrderBL
    {
         bool AddOrder(OrderModel model);
        OrderModel UpdateOrder(OrderModel model);
        List<OrderModel> GetOrder(int userId);
    }
}
