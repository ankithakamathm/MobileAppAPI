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
        Task<UserDTO> CheckLogin(string userName);

        Task<string> GetPassword(string userName);
        Task<UserDTO> RegisterUser(string username, string encryptedPassword, string email,string phoneNumber);

        Task<bool> checkIfUserExistsAlready(string phoneNumber);
        #endregion

        #region Category
        Task<CategoryDTO> GetAllCategories();

        #endregion



    }
}
