using BussinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class OrderBL: IOrderBL
    {
        IOrderRL orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;

        }

        public bool AddOrder(OrderModel model)
        {
            try
            {
                return this.orderRL.AddOrder(model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<OrderModel> GetOrder(int userId)
        {
            try
            {
                return this.orderRL.GetOrder(userId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public OrderModel UpdateOrder(OrderModel model)
        {
            try
            {
                return this.orderRL.UpdateOrder(model);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
