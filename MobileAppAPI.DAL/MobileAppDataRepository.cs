using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel;
using System.Reflection;
using MobileAppAPI.BLL;
using System.Globalization;
using MobileAppAPI.BLL.DTO;

namespace MobileAppAPI.DAL
{
    public class MobileAppDataRepository : IMobileAppDataRepository
    {
        private readonly IConfiguration _configuration;
        private string connectionString = string.Empty;
        public MobileAppDataRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
            connectionString = _configuration.GetConnectionString("MobileAppConnection");
        }

      
       
        #region User
       
        public async Task<UserDetailsDTO> GetAllUsers()
        {
            var model = new UserDetailsDTO();
            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    using (var sqlCommand = new SqlCommand("spGetAllAppUsers", sqlConnection))
                    {
                        DataTable dt = new DataTable();
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                       
                        var dataAdapter = new SqlDataAdapter(sqlCommand);

                        dataAdapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            model.UserDetails = dt.AsEnumerable().Select(dr =>
                            new UserDetailsInfo
                            {
                                Id = Convert.ToInt32(dr["Id"].ToString()),
                                FirstName = (dr["FirstName"] is DBNull || string.IsNullOrEmpty(dr["FirstName"].ToString())) ? "" : dr["FirstName"].ToString(),
                                LastName = (dr["LastName"] is DBNull || string.IsNullOrEmpty(dr["LastName"].ToString())) ? "" : dr["LastName"].ToString(),
                                PhoneNumber = (dr["Phone"] is DBNull || string.IsNullOrEmpty(dr["Phone"].ToString())) ? "" : dr["Phone"].ToString(),
                                Email = (dr["Email"] is DBNull || string.IsNullOrEmpty(dr["Email"].ToString())) ? "" : dr["Email"].ToString(),
                                Password = (dr["Password"] is DBNull || string.IsNullOrEmpty(dr["Password"].ToString())) ? "" : dr["Password"].ToString(),
                                IsAdmin = Convert.ToBoolean(dr["IsAdmin"].ToString()),
                                IsCustomer = Convert.ToBoolean(dr["IsCustomer"].ToString()),
                                

                            }).ToList();
                            model.IsSuccess = true;
                            model.Message = "info-fetch-getallusers-success";
                        }
                        else
                        {
                            model.IsSuccess = true;
                            model.Message = "nodata";
                        }
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                //Handle exception
                model.IsSuccess = false;
                model.Message = "error-fetch-getallusers-failed";
                return model;
            }
        }

        public async Task<Boolean> UpdatePassword(UserPersonalDetails details)
        {
            var success = false;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                using (var sqlCommand = new SqlCommand("usp_User_UpdatePassword", sqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@Email", details.Email);
                        sqlCommand.Parameters.AddWithValue("@NewPassword", details.NewPassword);
                        sqlCommand.Parameters.Add("@OutPut", SqlDbType.Int).Direction = ParameterDirection.Output;
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        if (Convert.ToInt32(sqlCommand.Parameters["@OutPut"].Value) > 0)
                        {
                            success = true;
                        }
                        sqlConnection.Open();
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
                return success;
            }
        }

        public async Task<UserDTO> CheckLogin(string userName)
        {
            var model = new UserDTO();
            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    using (var sqlCommand = new SqlCommand("spGetLoggedInUserData", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@UserName", userName);
                        //sqlCommand.Parameters.AddWithValue("@Password", password);
                        DataTable dt = new DataTable();
                        

                        var dataAdapter = new SqlDataAdapter(sqlCommand);

                        dataAdapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            var dr = dt.Rows[0];
                            model.UserInfo = new UserInfo
                            {
                                Id = Convert.ToInt32(dr["Id"].ToString()),
                                FirstName = (dr["First Name"] is DBNull || string.IsNullOrEmpty(dr["First Name"].ToString())) ? "" : dr["First Name"].ToString(),
                                LastName = (dr["Last Name"] is DBNull || string.IsNullOrEmpty(dr["Last Name"].ToString())) ? "" : dr["Last Name"].ToString(),
                                PhoneNumber = (dr["PhoneNumber"] is DBNull || string.IsNullOrEmpty(dr["PhoneNumber"].ToString())) ? "" : dr["PhoneNumber"].ToString(),
                                Email = (dr["Email"] is DBNull || string.IsNullOrEmpty(dr["Email"].ToString())) ? "" : dr["Email"].ToString(),
                                Password = (dr["Password"] is DBNull || string.IsNullOrEmpty(dr["Password"].ToString())) ? "" : dr["Password"].ToString(),
                                IsAdmin = Convert.ToBoolean(dr["IsAdmin"].ToString()),
                                IsCustomer = Convert.ToBoolean(dr["IsCustomer"].ToString()),
                                UserName= (dr["UserName"] is DBNull || string.IsNullOrEmpty(dr["UserName"].ToString())) ? "" : dr["UserName"].ToString(),

                            };
                            model.IsSuccess = true;
                            model.Message = "info-fetch-getallusers-success";
                        }
                        else
                        {
                            model.IsSuccess = true;
                            model.Message = "nodata";
                        }
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                //Handle exception
                model.IsSuccess = false;
                model.Message = "error-fetch-getallusers-failed";
                return model;
            }
        }

        public async Task<bool> checkIfUserExistsAlready(string phoneNumber)
        {
            bool result = false;
            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    using (var sqlCommand = new SqlCommand("spGetUserwithNumber", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                        //sqlCommand.Parameters.AddWithValue("@Password", password);
                        sqlConnection.Open();
                        result = Convert.ToInt32(sqlCommand.ExecuteScalar()) > 0 ? true : false;
                        sqlConnection.Close();
                    }
                       
                }
                return result;
            }
            catch (Exception ex)
            {
                //Handle exception
                return result;
            }
        }
        public async Task<UserDTO> RegisterUser(string username, string encryptedPassword, string email, string phoneNumber)
        {
            var model = new UserDTO();
            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    using (var sqlCommand = new SqlCommand("spRegisterNewUser", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@UserName", username);
                        sqlCommand.Parameters.AddWithValue("@Password", encryptedPassword);
                        sqlCommand.Parameters.AddWithValue("@Email", email);
                        sqlCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        DataTable dt = new DataTable();


                        var dataAdapter = new SqlDataAdapter(sqlCommand);

                        dataAdapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            var dr = dt.Rows[0];
                            model.UserInfo = new UserInfo
                            {
                                Id = Convert.ToInt32(dr["Id"].ToString()),
                                FirstName = (dr["First Name"] is DBNull || string.IsNullOrEmpty(dr["First Name"].ToString())) ? "" : dr["First Name"].ToString(),
                                LastName = (dr["Last Name"] is DBNull || string.IsNullOrEmpty(dr["Last Name"].ToString())) ? "" : dr["Last Name"].ToString(),
                                PhoneNumber = (dr["PhoneNumber"] is DBNull || string.IsNullOrEmpty(dr["PhoneNumber"].ToString())) ? "" : dr["PhoneNumber"].ToString(),
                                Email = (dr["Email"] is DBNull || string.IsNullOrEmpty(dr["Email"].ToString())) ? "" : dr["Email"].ToString(),
                                Password = (dr["Password"] is DBNull || string.IsNullOrEmpty(dr["Password"].ToString())) ? "" : dr["Password"].ToString(),
                                IsAdmin = Convert.ToBoolean(dr["IsAdmin"].ToString()),
                                IsCustomer = Convert.ToBoolean(dr["IsCustomer"].ToString()),
                                UserName = (dr["UserName"] is DBNull || string.IsNullOrEmpty(dr["UserName"].ToString())) ? "" : dr["UserName"].ToString(),

                            };
                            model.IsSuccess = true;
                            model.Message = "info-fetch-getallusers-success";
                        }
                        else
                        {
                            model.IsSuccess = true;
                            model.Message = "nodata";
                        }
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                //Handle exception
                model.IsSuccess = false;
                model.Message = "error-fetch-getallusers-failed";
                return model;
            }
        }
        public async Task<string> GetPassword(string userName)
        {
            var password = "";
            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    using (var sqlCommand = new SqlCommand("spGetUserPassword", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@UserName", userName);
                        sqlConnection.Open();
                        password = Convert.ToString(sqlCommand.ExecuteScalar());
                        sqlConnection.Close();
                    }
                }
                return password;
            }
            catch (Exception ex)
            {
                //Handle exception
               
                return password;
            }
        }



        #endregion

        #region category
        public async Task<CategoryDTO> GetAllCategories()
        {
            var model = new CategoryDTO();
            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    using (var sqlCommand = new SqlCommand("spGetAllCategories", sqlConnection))
                    {
                        DataTable dt = new DataTable();
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        var dataAdapter = new SqlDataAdapter(sqlCommand);

                        dataAdapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            model.CategoryDetails = dt.AsEnumerable().Select(dr =>
                            new CategoryInfo
                            {
                                Id = Convert.ToInt32(dr["Id"].ToString()),
                                Name = (dr["Name"] is DBNull || string.IsNullOrEmpty(dr["Name"].ToString())) ? "" : dr["Name"].ToString(),
                                
                                Active = Convert.ToBoolean(dr["Active"].ToString()),
                                Image= (dr["Image"] is DBNull || string.IsNullOrEmpty(dr["Image"].ToString())) ? "" : dr["Image"].ToString(),

                            }).ToList();
                            model.IsSuccess = true;
                            model.Message = "info-fetch-getallcategories-success";
                        }
                        else
                        {
                            model.IsSuccess = true;
                            model.Message = "nodata";
                        }
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                //Handle exception
                model.IsSuccess = false;
                model.Message = "error-fetch-getallcategories-failed";
                return model;
            }
        }

        #endregion



    }
}
