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
    [Route("Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productsService;
        private readonly IConfiguration _configuration;
        private readonly DAL.LoggingHelper _logging;
        private readonly HttpContext _httpContext;

        public ProductsController(IProductService context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _productsService = context;
            _configuration = configuration;
            _logging = new DAL.LoggingHelper(_configuration);
            _httpContext = httpContextAccessor.HttpContext;
        }

        // GET: Categories/GetCategories
        [HttpGet]
        [Route("GetProductsByCategory")]
        public async Task<ActionResult> GetAllProductsByCategory(int id)
        {


            try
            {
                var responseDTO = await _productsService.GetAllProductsByCategory(id);
                if (responseDTO.IsSuccess)
                {
                    return new OkObjectResult(new { MessageKey = "success", Result = new { Products = responseDTO.ProductDetails } });
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
