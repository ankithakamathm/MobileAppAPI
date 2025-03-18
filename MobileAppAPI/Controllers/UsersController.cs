using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MobileAppAPI.Models;
using MobileAppAPI.DAL;
using MobileAppAPI.BLL.Interfaces;
using MobileAppAPI.WebApi.Helpers;
using Helper = MobileAppAPI.WebApi.Helpers.Helper;
using MobileAppAPI.BLL;
using System.Data;

namespace MobileAppAPI.Controllers
{
    [Route("Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        private readonly DAL.LoggingHelper _logging;
        private readonly HttpContext _httpContext;

        public UsersController(IAccountService context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = context;
            _configuration = configuration;
            _logging = new DAL.LoggingHelper(_configuration);
            _httpContext = httpContextAccessor.HttpContext;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
           
          
            try
            {
                var responseDTO = await _accountService.GetAllUsers();
                if (responseDTO.IsSuccess)
                {
                    return new OkObjectResult(new { MessageKey = "success", Result = new { Users = responseDTO.UserDetails } });
                }
                else
                {
                    return new InternalServerErrorObjectResult(new { MessageKey = responseDTO.Message });
                }
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { MessageKey = e.Message });
            }
        }
        [HttpGet]
        [Route("Login")]
        public async Task<ActionResult> login(string username, string password)
        {


            try
            {
                var userPassword = await _accountService.GetPassword(username);
                var decryptedPassword = Helper.Decrypt(userPassword, _configuration["EncryptionKey"]);
                if (password == decryptedPassword)
                {
                    var responseDTO = await _accountService.CheckLogin(username);
                    if (responseDTO.IsSuccess)
                    {
                        return new OkObjectResult(new { MessageKey = "success", Result = new { Users = responseDTO.UserInfo } });
                    }
                    else
                    {
                        return new InternalServerErrorObjectResult(new { MessageKey = responseDTO.Message });
                    }
                }
                else
                {
                    return new InternalServerErrorObjectResult(new { Messagekey = "Username or Password is wrong" });
                }
            }
            catch (Exception e)
            {
                return new InternalServerErrorObjectResult(new { MessageKey = e.Message });
            }
        }


        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(string name, string password, string email, string mobile, int customerId)
        {


            try
            {
                bool res = await _accountService.checkIfUserExistsAlready(mobile);

                if (!res) {
                    var encryptedPassword = Helper.Encrypt(password, _configuration["EncryptionKey"]);

                    var responseDTO = await _accountService.RegisterUser(name, encryptedPassword, email, mobile, customerId);
                    if (responseDTO.IsSuccess)
                    {
                        return new OkObjectResult(new { MessageKey = "success", Result = new { Users = responseDTO.UserInfo } });
                    }
                    else
                    {
                        return new InternalServerErrorObjectResult(new { MessageKey = responseDTO.Message });
                    }
                }
                else
                {
                    return new InternalServerErrorObjectResult(new { Messagekey = "User with same phone number exists already" });
                }
            


            }
            catch (Exception e)
            {
                return new InternalServerErrorObjectResult(new { MessageKey = e.Message });
            }
        }


        [HttpPost]
        [Route("AddEditAddress")]
        public async Task<ActionResult> AddEditAddress(AddressDTO address)
        {
            try
            {

                var responseDTO = await _accountService.AddEditAddress(address);
                if (responseDTO.IsSuccess)
                {
                    return new OkObjectResult(new { MessageKey = "success" });
                }
                else
                {
                    return new InternalServerErrorObjectResult(new { MessageKey = responseDTO.Message });
                }
            }
            catch (Exception e)
            {
                return new InternalServerErrorObjectResult(new { MessageKey = e.Message });
            }
        }

        [HttpPost]
        [Route("DeleteAddress")]
        public async Task<ActionResult> DeletetAddress(AddressDTO address)
        {
            try
            {

                var responseDTO = await _accountService.DeleteAddress(address);
                if (responseDTO)
                {
                    return new OkObjectResult(new { MessageKey = "success" });
                }
                else
                {
                    return new InternalServerErrorObjectResult(new { MessageKey = responseDTO });
                }
            }
            catch (Exception e)
            {
                return new InternalServerErrorObjectResult(new { MessageKey = e.Message });
            }
        }


        [HttpGet]
        [Route("GetAddressesByUserId")]
        public async Task<ActionResult> GetAddressesByUserId(int userId)
        {


            try
            {
                var responseDTO = await _accountService.GetAddressesByUserId(userId);
                if (responseDTO.IsSuccess)
                {
                    return new OkObjectResult(new { MessageKey = "success", Result = new { Address = responseDTO.ListAddress } });
                }
                else
                {
                    return new InternalServerErrorObjectResult(new { MessageKey = responseDTO.Message });
                }
            }
            catch (Exception e)
            {
                return new OkObjectResult(new { MessageKey = e.Message });
            }
        }



    }
}
