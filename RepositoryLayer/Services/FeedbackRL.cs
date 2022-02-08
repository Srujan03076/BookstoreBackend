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
    public class FeedbackRL:IFeedbackRL
    {
        public FeedbackRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }
        MySqlConnection mysqlConnection;
        public bool AddFeedback(FeedbackModel model)
        {
            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spForAddingFeedback", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("p_FeedbackId", model.FeedbackId);
                    cmd.Parameters.AddWithValue("p_UserId", model.UserId);
                    cmd.Parameters.AddWithValue("p_BookId", model.BookId);
                    cmd.Parameters.AddWithValue("p_Comments", model.Comments);
                    cmd.Parameters.AddWithValue("p_Rating", model.Rating);
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
        public List<FeedbackModel> GetFeedback(int UserId)
        {
            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand(" spForGettingFeedback", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("p_UserId",UserId);
                    List<FeedbackModel> feedback = new List<FeedbackModel>();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            FeedbackModel feedbackmodel = new FeedbackModel();
                            feedbackmodel.FeedbackId = Convert.ToInt32(dr["FeedbackId"]);
                            feedbackmodel.UserId = Convert.ToInt32(dr["UserId"]);
                            feedbackmodel.BookId = Convert.ToInt32(dr["BookId "]);
                            feedbackmodel.Comments = dr["Comments"].ToString();
                            feedbackmodel.Rating = Convert.ToInt32(dr["Rating"]);
                            feedback.Add(feedbackmodel);
                        }
                        return feedback;
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


