using MobileAppAPI.BLL;
using MobileAppAPI.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MobileAppAPI.BLL
{
    public interface IMobileAppDataRepository
    {

        #region AppUser
       
        
        Task<UserDetailsDTO> GetAllUsers();
        Task<Boolean> UpdatePassword(UserPersonalDetails details);
        Task<AddressDTO> AddEditAddress(AddressDTO address);
        Task<UserDTO> CheckLogin(string userName);

        Task<string> GetPassword(string userName);
        Task<UserDTO> RegisterUser(string username, string encryptedPassword, string email,string phoneNumber);

        Task<bool> checkIfUserExistsAlready(string phoneNumber);

        Task<AddressDTO> GetAddressesByUserId(int userId);
        
        #endregion

        #region Category
        Task<CategoryDTO> GetAllCategories();

        #endregion

        #region SubCategory
        Task<SubCategoryDTO> GetAllSubCategories();

        #endregion

        #region Products
        Task<ProductDTO> GetAllProductsByCategory(int id);
        Task<ProductDTO> GetAllProductsMatchingSearch(string product);
        Task<int> CheckIfProductAvailable(int id);

        #endregion

        #region Orders
        Task<OrderDTO> SaveOrderByUser(OrderDTO order, DataTable dtOrders);
        Task<OrderDTO> GetAllOrdersById(int userId);
        // Task<OrderDTO> GetAllOrders();
        #endregion


        #region Theme
        Task<ThemesDTO> GetThemeById(int id);
        #endregion
    }
}
