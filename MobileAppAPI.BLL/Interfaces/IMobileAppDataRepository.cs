using MobileAppAPI.BLL;
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
        #endregion





    }
}
