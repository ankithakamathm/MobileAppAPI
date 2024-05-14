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
    [Route("SubCategory")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;
        private readonly IConfiguration _configuration;
        private readonly DAL.LoggingHelper _logging;
        private readonly HttpContext _httpContext;

        public SubCategoryController(ISubCategoryService context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _subCategoryService = context;
            _configuration = configuration;
            _logging = new DAL.LoggingHelper(_configuration);
            _httpContext = httpContextAccessor.HttpContext;
        }

        // GET: SubCategory/GetSubCategories
        [HttpGet]
        [Route("GetSubCategories")]
        public async Task<ActionResult> GetSubCategories()
        {


            try
            {
                var responseDTO = await _subCategoryService.GetAllSubCategories();
                if (responseDTO.IsSuccess)
                {
                    return new OkObjectResult(new { MessageKey = "success", Result = new { SubCategories = responseDTO.SubCategoryDetails } });
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
