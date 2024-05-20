using MobileAppAPI.BLL;
using System;
using System.Collections.Generic;
using System.Data;
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

        public Task<UserDTO> RegisterUser(string username, string encryptedPassword, string email, string phoneNumber)
        {
            return repository.RegisterUser(username, encryptedPassword, email, phoneNumber);
        }

        public Task<bool> checkIfUserExistsAlready(string phoneNumber)
        {
            return repository.checkIfUserExistsAlready(phoneNumber);
        }
        public Task<AddressDTO> AddAddress(AddressDTO address)
        {
            return repository.AddAddress(address);
        }

        
    }

}
