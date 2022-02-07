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
    public class WishlistRL : IWishlistRL
    {
        public WishlistRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }
        MySqlConnection mysqlConnection;
        public bool AddToWishList(WishlistModel model)
        {

            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spForCreatingWishlist", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("j_WishlistId", model.WishlistId);
                    cmd.Parameters.AddWithValue("j_UserId", model.UserId);
                    cmd.Parameters.AddWithValue("j_BookId", model.BookId);
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
        public bool DeleteWishlist(int WishlistId)
        {
            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spForDeletingBookFromWishlist", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("j_WishlistId", WishlistId);
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
        public BookModel GetBook(int BookId)
        {
            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand(" spForGetBook", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("r_BookId", BookId);
                    BookModel bookmodel = new BookModel();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        bookmodel.BookName = dr["BookName"].ToString();
                        bookmodel.AuthorName = dr["AuthorName"].ToString();
                        bookmodel.BookDescription = dr["BookDescription"].ToString();
                        bookmodel.BookImage = dr["BookImage"].ToString();
                        bookmodel.Quantity = Convert.ToInt32(dr["Quantity"]);
                        bookmodel.OriginalPrice = Convert.ToInt32(dr["OriginalPrice"]);
                        bookmodel.DiscountPrice = Convert.ToInt32(dr["DiscountPrice"]);
                        bookmodel.RatingCount = Convert.ToInt32(dr["RatingCount"]);

                    }
                    return bookmodel;
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
        
        


    

