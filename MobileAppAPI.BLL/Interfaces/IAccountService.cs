using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppAPI.BLL
{
    public interface IAccountService
    {
        
        //Task<long> AddUpdateCompanyUser(UserDetails userModel, long userId);

        //Task<UserDetailsDTO> GetCompanyUser(long userSK, long userId);

        //Task<UserDetailsDTO> GetAllCompanyUser(long companyId, long userId);
        Task<UserDetailsDTO> GetAllUsers();
        Task<UserDTO> CheckLogin(string userName, string password);
        // Task<long> AddUserCompanyRole(long id, long companySk, long userSK, long companyRoleSk, long userId);

        Task<Boolean> UpdatePassword(UserPersonalDetails details);
        Task<string> GetPassword(string username);


    }
}
