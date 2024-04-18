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
using MobileAppAPI.BLL;
using MobileAppAPI.WebApi.Helpers;
using Helper = MobileAppAPI.WebApi.Helpers.Helper;

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
                    var responseDTO = await _accountService.CheckLogin(username, userPassword);
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
                    ; return new InternalServerErrorObjectResult(new { Messagekey = "Username or Password is wrong" });
                }
            }
            catch (Exception e)
            {
                return new InternalServerErrorObjectResult(new { MessageKey = e.Message });
            }
        }



    }
}
