﻿using MobileAppAPI.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppAPI.BLL.Interfaces
{
    public class AccountServiceProvider : IAccountService
    {
        private readonly IMobileAppDataRepository repository;
        public AccountServiceProvider(IMobileAppDataRepository repository)
        {
            this.repository = repository;
        }

      
        //public Task<UserDetailsDTO> GetCompanyUser(long userSK, long userId)
        //{
        //    return repository.GetCompanyUser(userSK, userId);
        //}
        //public Task<UserDetailsDTO> GetAllCompanyUser(long companyId, long userId)
        //{
        //    return repository.GetAllCompanyUser(companyId, userId);
        //}

        public Task<UserDetailsDTO> GetAllUsers()
        {
            return repository.GetAllUsers();
        }

        public Task<Boolean> UpdatePassword(UserPersonalDetails details)
        {
            return repository.UpdatePassword(details);
        }
        public Task<UserDTO> CheckLogin(string userName)
        {
            return repository.CheckLogin(userName);
        }

        public Task<string> GetPassword(string userName)
        {
            return repository.GetPassword(userName);
        }

        public Task<UserDTO> RegisterUser(string username, string encryptedPassword, string email, string phoneNumber, int customerId)
        {
            return repository.RegisterUser(username, encryptedPassword, email, phoneNumber, customerId);
        }

        public Task<bool> checkIfUserExistsAlready(string phoneNumber)
        {
            return repository.checkIfUserExistsAlready(phoneNumber);
        }
        public Task<AddressDTO> AddEditAddress(AddressDTO address)
        {
            return repository.AddEditAddress(address);
        }

        public Task<AddressDTO> GetAddressesByUserId(int userId)
        {
            return repository.GetAddressesByUserId(userId);
        }

        public Task<bool> DeleteAddress(AddressDTO address)
        {
            return repository.DeleteAddress(address);
        }
    }

}
