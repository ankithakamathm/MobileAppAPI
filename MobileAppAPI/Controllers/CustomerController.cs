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
using MobileAppAPI.BLL.DTO;

namespace MobileAppAPI.Controllers
{
    [Route("Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        
        private readonly IThemeService _themeService;
        private readonly IConfiguration _configuration;
        private readonly DAL.LoggingHelper _logging;
        private readonly HttpContext _httpContext;

        public CustomerController(IThemeService context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _themeService = context;
            _configuration = configuration;
            _logging = new DAL.LoggingHelper(_configuration);
            _httpContext = httpContextAccessor.HttpContext;
        }

        [HttpGet]
        [Route("GetCustomerThemeById")]
        public async Task<ActionResult> GetCustomerThemeById(int customerId)
        {


            try
            {
                var responseDTO = await _themeService.GetThemeById(customerId);
                if (responseDTO.IsSuccess)
                {
                    return new OkObjectResult(new { MessageKey = "success", Result = new { Themes = responseDTO.ThemesDetails } });
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
