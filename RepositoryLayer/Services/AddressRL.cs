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
    public class AddressRL : IAddressRL
    {
        public AddressRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }
        MySqlConnection mysqlConnection;
        public bool AddAddress(AddressModel model)
        {

            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spForAddingAddress", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("p_AddressId", model.AddressId);
                    cmd.Parameters.AddWithValue("p_Address ", model.Address);
                    cmd.Parameters.AddWithValue("p_City", model.City);
                    cmd.Parameters.AddWithValue("p_State", model.State);
                    cmd.Parameters.AddWithValue("p_Addresstype", model.Addresstype);
                    cmd.Parameters.AddWithValue("p_UserId", model.UserId);
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
        public AddressModel UpdateAddress(AddressModel model)
        {
            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spForUpdatingAddress", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("p_AddressId", model.AddressId);
                    cmd.Parameters.AddWithValue("p_Address ", model.Address);
                    cmd.Parameters.AddWithValue("p_City", model.City);
                    cmd.Parameters.AddWithValue("p_State", model.State);
                    cmd.Parameters.AddWithValue("p_Addresstype", model.Addresstype);
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
        public List<AddressModel> GetAddress(int UserId)
        {
            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spForGettingAddressDetails", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("p_UserId", UserId);
                    List<AddressModel> address = new List<AddressModel>();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            AddressModel addressmodel = new AddressModel();
                            addressmodel.AddressId = dr.GetInt32("AddressId");
                            addressmodel.Address = dr.GetString("Address");
                            addressmodel.City = dr.GetString("City").ToString();
                            addressmodel.State = dr.GetString("State");
                            addressmodel.Addresstype = dr.GetString("Addresstype");
                            address.Add(addressmodel);
                        }
                        return address;
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

    

