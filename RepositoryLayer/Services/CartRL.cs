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
    public class CartRL : ICartRL
    {
        public CartRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }
        MySqlConnection mysqlConnection;

        public bool AddtoCart(CartModel model)
        {

            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spAddingBookToCart", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("q_BookId", model.BookId);
                    cmd.Parameters.AddWithValue("q_UserId", model.UserId);
                    cmd.Parameters.AddWithValue("q_Quantity", model.Quantity);
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
        public bool UpdateCart(int CartId, int Quantity)
        {

            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spForUpdatingCart", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("q_CartId", CartId);
                    cmd.Parameters.AddWithValue("q_Quantity", Quantity);
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
        public bool DeleteCart(int CartId)
        {
            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spForDeletingCartDetails", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("q_CartId", CartId);
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
                throw new Exception(e.Message);
            }
            finally
            {
                mysqlConnection.Close();
            }
        }
        public List<CartModel> GetCart(int UserId)
        {
            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spForGettingCartDetails", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("r_UserId", UserId);
                    List<CartModel> cart = new List<CartModel>();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CartModel cartmodel = new CartModel();
                            BookModel bookmodel = new BookModel();
                            cartmodel.CartId = Convert.ToInt32(dr["CartId"]);
                            cartmodel.BookId = Convert.ToInt32(dr["BookId"]);
                            cartmodel.UserId = Convert.ToInt32(dr["UserId"]);
                            cartmodel.Quantity = Convert.ToInt32(dr["Quantity"]);
                            bookmodel.BookName = dr["BookName"].ToString();
                            bookmodel.AuthorName = dr["AuthorName"].ToString();
                            bookmodel.BookDescription = dr["BookDescription"].ToString();
                            bookmodel.BookImage = dr["BookImage"].ToString();
                            bookmodel.OriginalPrice = Convert.ToInt32(dr["OriginalPrice"]);
                            bookmodel.DiscountPrice = Convert.ToInt32(dr["DiscountPrice"]);
                            bookmodel.RatingCount = Convert.ToInt32(dr["RatingCount"]);
                            cart.Add(cartmodel);
                        }
                        return cart;
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
