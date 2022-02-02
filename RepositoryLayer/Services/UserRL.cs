using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        public UserRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }
        MySqlConnection mysqlConnection;
        public string EncryptPassword(string password)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);
        }
        public bool UserRegistration(RegistrationModel model)
        {

            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {


                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spForAddingUsers", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("p_UserId", model.UserId);
                    cmd.Parameters.AddWithValue("p_FulName", model.FullName);
                    cmd.Parameters.AddWithValue("p_EmailId", model.EmailId);
                    cmd.Parameters.AddWithValue("p_Password", EncryptPassword(model.Password));
                    cmd.Parameters.AddWithValue("p_MobileNumber", model.MobileNumber);
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
        public Loginmodel Login(Loginmodel login)
        {
            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));

            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spUserLogin", mysqlConnection);
                    login.Password = EncryptPassword(login.Password);
                    string token;
                    token = GenerateToken(login.EmailId);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("p_EmailId", login.EmailId);
                    cmd.Parameters.AddWithValue("p_Password", login.Password);
                    cmd.Parameters.Add("status", MySqlDbType.Int16).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    var result = cmd.Parameters["status"].Value;
                    if (!(result is DBNull))
                    {
                        if (Convert.ToInt32(result) == 1)
                        {
                            return login;
                        }
                    }
                    return login;

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
        public string GenerateToken(string EmailId)
        {
            try
            {
                var key = Encoding.UTF8.GetBytes(this.Configuration["SecretKey"]);
                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
                SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, EmailId) }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
                };
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
                return handler.WriteToken(token);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
    

            

            
   

            
    



    

        
    




    

















          