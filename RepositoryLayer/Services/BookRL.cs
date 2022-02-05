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
    public class BookRL : IBookRL
    {
        public BookRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }
        MySqlConnection mysqlConnection;

        public BookModel AddBook(BookModel model)
        {

            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spForAddingBookDetails", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("r_BookName", model.BookName);
                    cmd.Parameters.AddWithValue("r_AuthorName", model.AuthorName);
                    cmd.Parameters.AddWithValue("r_BookDescription", model.BookDescription);
                    cmd.Parameters.AddWithValue("r_BookImage", model.BookImage);
                    cmd.Parameters.AddWithValue("r_Quantity", model.Quantity);
                    cmd.Parameters.AddWithValue("r_OriginalPrice", model.OriginalPrice);
                    cmd.Parameters.AddWithValue("r_DiscountPrice", model.DiscountPrice);
                    cmd.Parameters.AddWithValue("r_RatingCount", model.RatingCount);
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
                throw;
            }
            finally
            {
                mysqlConnection.Close();
            }
        }
        public BookModel UpdateBook(BookModel model)
        {
            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spForUpdatingBookDetails", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("r_BookId", model.BookId);
                    cmd.Parameters.AddWithValue("r_BookName", model.BookName);
                    cmd.Parameters.AddWithValue("r_AuthorName", model.AuthorName);
                    cmd.Parameters.AddWithValue("r_BookDescription", model.BookDescription);
                    cmd.Parameters.AddWithValue("r_BookImage ", model.BookImage);
                    cmd.Parameters.AddWithValue("r_Quantity", model.Quantity);
                    cmd.Parameters.AddWithValue("r_OriginalPrice", model.OriginalPrice);
                    cmd.Parameters.AddWithValue("r_DiscountPrice", model.DiscountPrice);
                    cmd.Parameters.AddWithValue(" r_RatingCount", model.RatingCount);
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
        public bool DeleteBook(int BookId)
        {
            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spForDeletingBookDetails", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("r_BookId", BookId);
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
        public BookModel GetBook(int bookId)
        {
            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand(" s pForGetBook", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("r_BookId", bookId);
                    BookModel bookmodel = new BookModel();
                    MySqlDataReader read = cmd.ExecuteReader();
                    if(read.Read())
                    {
                        bookmodel.BookName = read["BookName"].ToString();
                        bookmodel.AuthorName = read["AuthorName"].ToString();
                        bookmodel.BookDescription = read["BookDescription"].ToString();
                        bookmodel.BookImage = read["BookImage"].ToString();
                        bookmodel.Quantity = Convert.ToInt32(read["Quantity"]);
                        bookmodel.OriginalPrice = Convert.ToInt32(read["OriginalPrice"]);
                        bookmodel.DiscountPrice = Convert.ToInt32(read["DiscountPrice"]);
                        bookmodel.RatingCount = Convert.ToInt32(read["RatingCount"]);
                        return bookmodel;
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

    





