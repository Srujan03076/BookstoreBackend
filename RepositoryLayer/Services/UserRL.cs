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
using System.Net.Mail;
using Experimental.System.Messaging;
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
        public string ForgotPassword(string EmailId)
        {
            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {

                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spForForgotPassword", mysqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_EmailId", EmailId);
                    mysqlConnection.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        SMTP(EmailId);
                        return "Email is sent successfully";
                    }
                    else
                    {
                        return "Email Id does not exist";
                    }

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void SMTP(string EmailId)
        {
            MailMessage mailId = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mailId.From = new MailAddress(this.Configuration["Credentials:testEmailId"]);
            mailId.To.Add(EmailId);
            mailId.Subject = "Test Mail";
            this.SendMSMQ();
            mailId.Body = this.ReceiveMSMQ();
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(this.Configuration["Credentials:testEmailId"], this.Configuration["Credentials:testEmailPassword"]);
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mailId);
        }
        public void SendMSMQ()
        {
            MessageQueue msgQueue;
            if (MessageQueue.Exists(@".\Private$\books"))
            {
                msgQueue = new MessageQueue(@".\Private$\books");
            }
            else
            {
                msgQueue = MessageQueue.Create(@".\Private$\books");
            }
            Message message = new Message();
            var formatter = new BinaryMessageFormatter();
            message.Formatter = formatter;
            message.Body = "This mail is to reset password";
            msgQueue.Label = "MailBody";
            msgQueue.Send(message);
        }
        public string ReceiveMSMQ()
        {
            var receivequeue = new MessageQueue(@".\Private$\books");
            var receivemsg = receivequeue.Receive();
            receivemsg.Formatter = new BinaryMessageFormatter();
            return receivemsg.Body.ToString();
        }
        public ResetPasswordModel ResetPassword(ResetPasswordModel resetPassword)
        {
            mysqlConnection = new MySqlConnection(this.Configuration.GetConnectionString("bookstore"));
            try
            {
                using (mysqlConnection)
                {
                    MySqlCommand cmd = new MySqlCommand("spUserResetPassword", mysqlConnection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    mysqlConnection.Open();
                    cmd.Parameters.AddWithValue("p_UserId", resetPassword.UserId);
                    cmd.Parameters.AddWithValue("p_NewPassword", EncryptPassword(resetPassword.Password));
                    cmd.ExecuteNonQuery();

                    return resetPassword;
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


        
        
        
            
            
               
               
           
               
    


    
    











































          