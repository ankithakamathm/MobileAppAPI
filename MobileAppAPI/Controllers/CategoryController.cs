using Microsoft.AspNetCore.Mvc;
using MobileAppAPI.BLL;
using MobileAppAPI.WebApi.Helpers;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MobileAppAPI.BLL.Interfaces;



namespace MobileAppAPI.Controllers
{
    [Route("Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IConfiguration _configuration;
        private readonly DAL.LoggingHelper _logging;
        private readonly HttpContext _httpContext;

        public CategoryController(ICategoryService context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _categoryService = context;
            _configuration = configuration;
            _logging = new DAL.LoggingHelper(_configuration);
            _httpContext = httpContextAccessor.HttpContext;
        }

        // GET: Categories/GetCategories
        [HttpGet]
        [Route("GetCategories")]
        public async Task<ActionResult> GetCategories()
        {


            try
            {
                var responseDTO = await _categoryService.GetAllCategories();
                if (responseDTO.IsSuccess)
                {
                    return new OkObjectResult(new { MessageKey = "success", Result = new { Users = responseDTO.CategoryDetails } });
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
