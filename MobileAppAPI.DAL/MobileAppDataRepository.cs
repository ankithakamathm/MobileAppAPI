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
                        success = true;
                        
                      //  sqlConnection.Close();
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


        public async Task<AddressDTO> AddEditAddress(AddressDTO address)
        {
            var model = new AddressDTO();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                using (var sqlCommand = new SqlCommand("spUserAddEditAddress", sqlConnection))
                {
                    try
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@Id", address.AddAddress.Id);
                        sqlCommand.Parameters.AddWithValue("@Address", address.AddAddress.UserAddress);
                        sqlCommand.Parameters.AddWithValue("@Name", address.AddAddress.Name);
                        sqlCommand.Parameters.AddWithValue("@City", address.AddAddress.City);
                        sqlCommand.Parameters.AddWithValue("@State", address.AddAddress.State);
                        sqlCommand.Parameters.AddWithValue("@Email", address.AddAddress.Email);
                        sqlCommand.Parameters.AddWithValue("@UserId", address.AddAddress.UserId);
                        sqlCommand.Parameters.AddWithValue("@Phone", address.AddAddress.Phone);

                        sqlCommand.Parameters.AddWithValue("@Pincode", address.AddAddress.Pincode);
                        
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        
                        model.IsSuccess = true;
                        model.Message = "info-fetch-addaddress-success";


                        return model;
                        //  sqlConnection.Close();
                    }
                    catch (Exception ex)
                    {
                        
                        model.IsSuccess = false;
                        //model.Message = "info-fetch-addaddress-failure";
                        return model;
                        //Handle exception
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
                
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

        public async Task<AddressDTO> GetAddressesByUserId(int userId)
        {
            var model = new AddressDTO();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                using (var sqlCommand = new SqlCommand("spGetAllAddressesForUser", sqlConnection))
                {
                    try
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@UserId", userId);
                        var dataAdapter = new SqlDataAdapter(sqlCommand);

                        dataAdapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            model.ListAddress = dt.AsEnumerable().Select(dr =>
                            new Address
                            {
                                Id = Convert.ToInt32(dr["Id"].ToString()),
                                UserAddress = (dr["Address"] is DBNull || string.IsNullOrEmpty(dr["Address"].ToString())) ? "" : dr["Address"].ToString(),
                                City = (dr["City"] is DBNull || string.IsNullOrEmpty(dr["City"].ToString())) ? "" : dr["City"].ToString(),
                                State = (dr["State"] is DBNull || string.IsNullOrEmpty(dr["State"].ToString())) ? "" : dr["State"].ToString(),
                                Pincode = (dr["Pincode"] is DBNull || string.IsNullOrEmpty(dr["Pincode"].ToString())) ? "" : dr["Pincode"].ToString(),
                                Phone = (dr["Phone"] is DBNull || string.IsNullOrEmpty(dr["Phone"].ToString())) ? "" : dr["Phone"].ToString(),
                                UserId = Convert.ToInt32(dr["UserId"].ToString()),
                                Email = (dr["Email"] is DBNull || string.IsNullOrEmpty(dr["Email"].ToString())) ? "" : dr["Email"].ToString(),
                                Name = (dr["Name"] is DBNull || string.IsNullOrEmpty(dr["Name"].ToString())) ? "" : dr["Name"].ToString(),

                            }).ToList();
                            model.IsSuccess = true;
                            model.Message = "info-fetch-getaddressesbyuserid-success";
                        }
                        else
                        {
                            model.IsSuccess = true;
                            model.Message = "nodata";
                        }

                        
                        return model;
                    }

                    catch (Exception ex)
                    {
                        //Handle exception
                        model.IsSuccess = false;
                        //model.Message = "error-fetch-saveorderbyuser-failed";
                        model.Message = "error-fetch-getaddressesbyuserid-failed";
                        return model;
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }

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
                                Image= (dr["Image"] is DBNull || string.IsNullOrEmpty(dr["Image"].ToString())) ? null : (byte[])dr["Image"],

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


        #region subcategory
        public async Task<SubCategoryDTO> GetAllSubCategories()
        {
            var model = new SubCategoryDTO();
            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    using (var sqlCommand = new SqlCommand("spGetAllSubCategories", sqlConnection))
                    {
                        DataTable dt = new DataTable();
                        sqlCommand.CommandType = CommandType.StoredProcedure;

                        var dataAdapter = new SqlDataAdapter(sqlCommand);

                        dataAdapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            model.SubCategoryDetails = dt.AsEnumerable().Select(dr =>
                            new SubCategoryInfo
                            {
                                Id = Convert.ToInt32(dr["Id"].ToString()),
                                Name = (dr["Name"] is DBNull || string.IsNullOrEmpty(dr["Name"].ToString())) ? "" : dr["Name"].ToString(),
                                CategoryId= Convert.ToInt32(dr["CategoryId"].ToString()),
                                Active = Convert.ToBoolean(dr["Active"].ToString()),
                                Image = (dr["Image"] is DBNull || string.IsNullOrEmpty(dr["Image"].ToString())) ? null : (byte[])dr["Image"],

                            }).ToList();
                            model.IsSuccess = true;
                            model.Message = "info-fetch-getallsubcategories-success";
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
                model.Message = "error-fetch-getallsubcategories-failed";
                return model;
            }
        }

        #endregion

        #region Products
        public async Task<ProductDTO> GetAllProductsByCategory(int id)
        {
            var model = new ProductDTO();
            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    using (var sqlCommand = new SqlCommand("spGetAllProducts", sqlConnection))
                    {
                        DataTable dt = new DataTable();
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@CategoryId", id);
                        var dataAdapter = new SqlDataAdapter(sqlCommand);

                        dataAdapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            model.ProductDetails = dt.AsEnumerable().Select(dr =>
                            new ProductDTOInfo
                            {
                                Id = Convert.ToInt32(dr["Id"].ToString()),
                                CategoryId = Convert.ToInt32(dr["CategoryId"].ToString()),
                                SubCategoryId = Convert.ToInt32(dr["SubCategoryId"].ToString()),
                                Name = (dr["ProductName"] is DBNull || string.IsNullOrEmpty(dr["ProductName"].ToString())) ? "" : dr["ProductName"].ToString(),
                                Description = (dr["Description"] is DBNull || string.IsNullOrEmpty(dr["Description"].ToString())) ? "" : dr["Description"].ToString(),
                                Price = Convert.ToDouble(dr["Price"].ToString()),
                                Available = Convert.ToBoolean(dr["Available"].ToString()),
                                Image = (dr["Image"] is DBNull || string.IsNullOrEmpty(dr["Image"].ToString())) ? null : (byte[])dr["Image"],
                                //Attribute=(dr["Attribute"] is DBNull || string.IsNullOrEmpty(dr["Attribute"].ToString())) ? "" : dr["Attribute"].ToString(),
                                Currency = (dr["Currency"] is DBNull || string.IsNullOrEmpty(dr["Currency"].ToString())) ? "" : dr["Currency"].ToString(),
                                QuantityAvailable= Convert.ToInt32(dr["QuantityAvailable"].ToString()),
                            }).ToList();
                            model.IsSuccess = true;
                            model.Message = "info-fetch-getallproducts-success";
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
                model.Message = "error-fetch-getallproducts-failed";
                return model;
            }
        }
        public async Task<int> CheckIfProductAvailable(int id)
        {
            int count = 0;
            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    using (var sqlCommand = new SqlCommand("spGetAvailableCountofProducts", sqlConnection))
                    {
                        DataTable dt = new DataTable();
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@ProductId", id);
                        sqlCommand.Connection.Open();
                        count = Convert.ToInt32(sqlCommand.ExecuteScalar());
                        sqlCommand.Connection.Close();
                    }
                }
                
            }
            catch (Exception ex)
            {
                //Handle exception
                count = -1;
            }
            return count;
        }

        public async Task<ProductDTO> GetAllProductsMatchingSearch(string product)
        {
            var model = new ProductDTO();
            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    using (var sqlCommand = new SqlCommand("spGetAllProductsBySearch", sqlConnection))
                    {
                        DataTable dt = new DataTable();
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@ProductName", product);
                        var dataAdapter = new SqlDataAdapter(sqlCommand);

                        dataAdapter.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            model.ProductDetails = dt.AsEnumerable().Select(dr =>
                            new ProductDTOInfo
                            {
                                Id = Convert.ToInt32(dr["Id"].ToString()),
                                CategoryId = Convert.ToInt32(dr["CategoryId"].ToString()),
                                SubCategoryId = Convert.ToInt32(dr["SubCategoryId"].ToString()),
                                Name = (dr["ProductName"] is DBNull || string.IsNullOrEmpty(dr["ProductName"].ToString())) ? "" : dr["ProductName"].ToString(),
                                Description = (dr["Description"] is DBNull || string.IsNullOrEmpty(dr["Description"].ToString())) ? "" : dr["Description"].ToString(),
                                Price = Convert.ToDouble(dr["Price"].ToString()),
                                Available = Convert.ToBoolean(dr["Available"].ToString()),
                                Image = (dr["Image"] is DBNull || string.IsNullOrEmpty(dr["Image"].ToString())) ? null : (byte[])dr["Image"],
                                //Attribute = (dr["Attribute"] is DBNull || string.IsNullOrEmpty(dr["Attribute"].ToString())) ? "" : dr["Attribute"].ToString(),
                                Currency = (dr["Currency"] is DBNull || string.IsNullOrEmpty(dr["Currency"].ToString())) ? "" : dr["Currency"].ToString(),
                                QuantityAvailable = Convert.ToInt32(dr["QuantityAvailable"].ToString()),
                            }).ToList();
                            model.IsSuccess = true;
                            model.Message = "info-fetch-getallproductsbysearch-success";
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
                //model.IsSuccess = false;
                model.Message = "error-fetch-getallproductsbysearch-failed";
                return model;
            }
        }
        #endregion

        #region Orders
        public async Task<OrderDTO> SaveOrderByUser(OrderDTO order, DataTable dtOrders)
        {
            var model = new OrderDTO();
           
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    using (var sqlCommand = new SqlCommand("spSaveOrderByUser", sqlConnection))
                    {
                    try
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@Id", order.OrderDetails.Id);
                        sqlCommand.Parameters.AddWithValue("@Name", order.OrderDetails.UserName);


                        sqlCommand.Parameters.AddWithValue("@Email", order.OrderDetails.Email);
                        
                        sqlCommand.Parameters.AddWithValue("@PhoneNumber", order.OrderDetails.Mobile);
                        sqlCommand.Parameters.AddWithValue("@City", order.OrderDetails.City);
                        sqlCommand.Parameters.AddWithValue("@Address", order.OrderDetails.Address);
                        sqlCommand.Parameters.AddWithValue("@State", order.OrderDetails.State);
                        sqlCommand.Parameters.AddWithValue("@ZipCode", order.OrderDetails.zip_code);
                        sqlCommand.Parameters.AddWithValue("@TotalPrice", order.OrderDetails.TotalPrice);
                        sqlCommand.Parameters.AddWithValue("@OrderItems", dtOrders);
                        DataTable dt = new DataTable();
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();

                            model.IsSuccess = true;
                            model.Message = "info-fetch-saveorderbyuser-success";
                        
                       
                        return model;
                    }

                    catch (Exception ex)
                    {
                        //Handle exception
                        model.IsSuccess = false;
                        //model.Message = "error-fetch-saveorderbyuser-failed";
                        model.Message = ex.Message;
                        return model;
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
                
            }
           



        }
        public async Task<OrderDTO> GetAllOrdersById(int userId)
        {
            var model = new OrderDTO();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                using (var sqlCommand = new SqlCommand("spAllOrdersForUser", sqlConnection))
                {
                    try
                    {
                        DataSet ds = new DataSet();
                        
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@Id", userId);
                        var dataAdapter = new SqlDataAdapter(sqlCommand);

                        dataAdapter.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataTable dt = new DataTable();

                            dt = ds.Tables[0];
                            
                               
                            List<OrderInfo> orders = new List<OrderInfo>();
                            foreach (DataRow dr in dt.Rows)
                            {
                                OrderInfo orderInfo=new OrderInfo
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    UserName = dr["Name"].ToString(),
                                    Email = dr["Email"].ToString(),
                                    Mobile = dr["PhoneNumber"].ToString(),
                                    City = dr["City"].ToString(),
                                    Address = dr["Address"].ToString(),
                                    State = dr["State"].ToString(),
                                    Status = dr["OrderStatus"].ToString(),
                                    OrderedDate = Convert.ToDateTime(dr["OrderDate"]),
                                    zip_code = dr["ZipCode"].ToString(),
                                    TotalPrice = dr["TotalPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["TotalPrice"])
                                };
                                //DataView dv = new DataView(ds.Tables[1]);
                                //dv.RowFilter = "Id == Convert.ToInt32(row[\"OrderId\"])";
                                dt = ds.Tables[1];
                                
                                orderInfo.OrderItems = dt.AsEnumerable().Where(x => x.Field<int>("OrderId") == Convert.ToInt32(dr["Id"])).Select(dataRow =>
                                new OrderItems
                                {
                                    OrderItemId = Convert.ToInt32(dataRow["OrderId"]),
                                    ItemName = dataRow["ItemName"].ToString(),
                                    ItemQuantity = Convert.ToInt32(dataRow["Quantity"]),
                                    ItemPrice = Convert.ToDecimal(dataRow["Price"]),
                                    ItemTotal = Convert.ToDecimal(dataRow["SubTotal"]),
                                    Currency = dataRow["Currency"].ToString(),
                                    Image = (dataRow["Image"] is DBNull || string.IsNullOrEmpty(dataRow["Image"].ToString())) ? null : (byte[])dataRow["Image"],
                                    

                                }).ToList();
                                orders.Add(orderInfo);
                            }
                            model.OrderList = orders;
                            model.IsSuccess = true;
                            model.Message = "info-fetch-getallcategories-success";
                        }
                        else
                        {
                            model.IsSuccess = true;
                            model.Message = "nodata";
                        }

                        model.IsSuccess = true;
                        model.Message = "info-fetch-saveorderbyuser-success";


                        return model;
                    }

                    catch (Exception ex)
                    {
                        //Handle exception
                        model.IsSuccess = false;
                        //model.Message = "error-fetch-saveorderbyuser-failed";
                        model.Message = ex.Message;
                        return model;
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }

            }




        }
        //Task<OrderDTO> GetAllOrders()
        //{


        //}
        #endregion

        #region Theme
        public async Task<ThemesDTO> GetThemeById(int id)
        {

            ThemesDTO model = new ThemesDTO();
            try
            {
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    using (var sqlCommand = new SqlCommand("spGetThemeById", sqlConnection))
                    {
                        DataTable dt = new DataTable();
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@CustomerId", id);
                        sqlConnection.Open();
                        SqlDataReader dr = sqlCommand.ExecuteReader();

                        while (dr.Read())
                        {
                            model.ThemesDetails = new ThemesInfo
                            {
                                Id = Convert.ToInt32(dr["Id"].ToString()),
                                AppName = (dr["AppName"] is DBNull || string.IsNullOrEmpty(dr["AppName"].ToString())) ? "" : dr["AppName"].ToString(),
                                ColorCode = (dr["ColorCode"] is DBNull || string.IsNullOrEmpty(dr["ColorCode"].ToString())) ? "" : dr["ColorCode"].ToString(),
                                ButtonColor = (dr["ButtonColor"] is DBNull || string.IsNullOrEmpty(dr["ButtonColor"].ToString())) ? "" : dr["ButtonColor"].ToString(),
                                LogoImageName = (dr["LogoImageName"] is DBNull || string.IsNullOrEmpty(dr["LogoImageName"].ToString())) ? "" : dr["LogoImageName"].ToString(),
                                Description = (dr["Description"] is DBNull || string.IsNullOrEmpty(dr["Description"].ToString())) ? "" : dr["Description"].ToString(),
                                Logo = (dr["Logo"] is DBNull || string.IsNullOrEmpty(dr["Logo"].ToString())) ? null : (byte[])dr["Logo"],
                                CustomerId = Convert.ToInt32(dr["CustomerId"].ToString()),
                            };

                        }
                        sqlConnection.Close();
                        model.IsSuccess = true;
                        model.Message = "info-fetch-getthemebyid-success";

                    }

                    return model;
                }

            }
            catch (Exception ex)
            {
                //Handle exception
                model.IsSuccess = false;
                model.Message = "info-fetch-getthemebyid-failure";

                return model;
            }



        }
        #endregion
    }
}

