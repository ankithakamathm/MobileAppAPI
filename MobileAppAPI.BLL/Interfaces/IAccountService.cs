﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppAPI.BLL.Interfaces
{
    public interface IAccountService
    {
        
        //Task<long> AddUpdateCompanyUser(UserDetails userModel, long userId);

        //Task<UserDetailsDTO> GetCompanyUser(long userSK, long userId);

        //Task<UserDetailsDTO> GetAllCompanyUser(long companyId, long userId);
        Task<UserDetailsDTO> GetAllUsers();
        Task<UserDTO> CheckLogin(string userName);
        // Task<long> AddUserCompanyRole(long id, long companySk, long userSK, long companyRoleSk, long userId);

        Task<Boolean> UpdatePassword(UserPersonalDetails details);
        Task<AddressDTO> AddEditAddress(AddressDTO address);
        Task<bool> DeleteAddress(AddressDTO address);
        Task<string> GetPassword(string username);
        Task<UserDTO> RegisterUser(string username, string encryptedPassword, string email, string phoneNumber, int customerId);
        Task<bool> checkIfUserExistsAlready(string phoneNumber);

        Task<AddressDTO> GetAddressesByUserId(int userID);
    }
}
