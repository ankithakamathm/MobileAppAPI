
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MobileAppAPI.BLL.Models;
using MobileAppAPI.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;

namespace MobileAppAPI.DAL
{
    public class LoggingHelper
    {
        private readonly IConfiguration _configuration;
        private string connectionString = string.Empty;
        private bool enableLogging = false;
        private string ipaddress;

        public LoggingHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("MobileAppConnection");
            enableLogging = _configuration.GetValue<bool>("EnableLogging");

        }

        public void Logging(Log log, bool isSave, bool isDelete, HttpContext httpContext)
        {
            if ((enableLogging && isSave) || isDelete)
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    using (var sqlCommand = new SqlCommand("usp_Insert_Logs", sqlConnection))
                    {
                        try
                        {
                            string clientIpAddress = httpContext?.Connection?.RemoteIpAddress?.ToString();
                            string clientMachineName = httpContext?.Request?.Headers["X-Forwarded-Host"].FirstOrDefault();

                            // check for forwarded headers if application is behind a reverse proxy
                            if (httpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                            {
                                clientIpAddress = httpContext.Request.Headers.ContainsKey("X-Forwarded-For").ToString();
                            }
                            else
                            {
                                clientIpAddress = httpContext?.Connection?.RemoteIpAddress.MapToIPv4().ToString();
                            }
                            //string hostName = Dns.GetHostName();
                            //string ipaddress = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            sqlCommand.Parameters.AddWithValue("@description", SQLHelper.ToDbNull(log.Description));
                            sqlCommand.Parameters.AddWithValue("@message", SQLHelper.ToDbNull(log.Message));
                            sqlCommand.Parameters.AddWithValue("@exception", SQLHelper.ToDbNull(log.Exception));
                            sqlCommand.Parameters.AddWithValue("@user", SQLHelper.ToDbNull(log.User));
                            //sqlCommand.Parameters.AddWithValue("@CompanyName", SQLHelper.ToDbNull(Convert.ToDateTime(log.CreatedDate)));
                            sqlCommand.Parameters.AddWithValue("@hostName", SQLHelper.ToDbNull(clientMachineName));
                            sqlCommand.Parameters.AddWithValue("@ipaddress", SQLHelper.ToDbNull(clientIpAddress));
                            sqlConnection.Open();
                            sqlCommand.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            //Handle exception
                        }
                        finally
                        {
                            sqlConnection.Close();
                        }
                    }

                }
            }
        }

        public void ExceptionLogging(Log log)
        {

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                using (var sqlCommand = new SqlCommand("usp_Insert_ExceptionLogs", sqlConnection))
                {
                    try
                    {
                        string hostName = Dns.GetHostName();
                        string ipaddress = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@description", SQLHelper.ToDbNull(log.Description));
                        sqlCommand.Parameters.AddWithValue("@message", SQLHelper.ToDbNull(log.Message));
                        sqlCommand.Parameters.AddWithValue("@exception", SQLHelper.ToDbNull(log.Exception));
                        sqlCommand.Parameters.AddWithValue("@user", SQLHelper.ToDbNull(log.User));
                        //sqlCommand.Parameters.AddWithValue("@CompanyName", SQLHelper.ToDbNull(Convert.ToDateTime(log.CreatedDate)));
                        sqlCommand.Parameters.AddWithValue("@hostName", SQLHelper.ToDbNull(hostName));
                        sqlCommand.Parameters.AddWithValue("@ipaddress", SQLHelper.ToDbNull(ipaddress));
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        //Handle exception
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }


            }
        }
    }
}