using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RepositoryLayer.Services
{
    public class OrderRL : IOrderRL
    {
        public OrderRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }
        MySqlConnection mysqlConnection;
        public bool AddOrder(OrderModel model)
        {

            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spForAddingOrder", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("p_OrderId", model.OrderId);
                    cmd.Parameters.AddWithValue("p_UserId", model.UserId);
                    cmd.Parameters.AddWithValue("p_AddressId", model.AddressId);
                    cmd.Parameters.AddWithValue("p_BookId ", model.BookId);
                    cmd.Parameters.AddWithValue("p_TotalPrice ", model.TotalPrice);
                    cmd.Parameters.AddWithValue("p_BookQuantity ", model.BookQuantity);
                    cmd.Parameters.AddWithValue("p_OrderDate", model.OrderDate);
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                mysqlConnection.Close();
            }
        }
        public OrderModel UpdateOrder(OrderModel model)
        {
            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spForUpdatingOrder", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("p_OrderId", model.OrderId);
                    cmd.Parameters.AddWithValue("p_BookQuantity ", model.BookQuantity);
                    cmd.Parameters.AddWithValue("p_TotalPrice ", model.TotalPrice);
                    cmd.Parameters.AddWithValue("p_OrderDate", model.OrderDate);
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return model;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                mysqlConnection.Close();
            }
        }
        public List<OrderModel>GetOrder(int UserId)
        {
            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spForGettingOrder", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("r_UserId", UserId);
                    List<OrderModel> order = new List<OrderModel>();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            OrderModel ordermodel = new OrderModel();
                            ordermodel.OrderId = Convert.ToInt32(dr["OrderId"]);
                            ordermodel.UserId = Convert.ToInt32(dr["UserId"]);
                            ordermodel.AddressId = Convert.ToInt32(dr["AddressId"]);
                            ordermodel.BookId = Convert.ToInt32(dr["BookId"]);
                            ordermodel.TotalPrice = Convert.ToInt32(dr["TotalPrice"]);
                            ordermodel.BookQuantity = Convert.ToInt32(dr["BookQuantity"]);
                            ordermodel.OrderDate = Convert.ToInt32(dr["OrderDate"]);
                            order.Add(ordermodel);
                        }
                        return order;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                mysqlConnection.Close();
            }
        }
    }
}








                   

          
            