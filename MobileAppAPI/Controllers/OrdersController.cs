﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MobileAppAPI.BLL.Interfaces;
using MobileAppAPI.WebApi.Helpers;
using System.Threading.Tasks;
using System;
using MobileAppAPI.BLL.DTO;
using System.Data;

namespace MobileAppAPI.Controllers
{
    [Route("Orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;
        private readonly DAL.LoggingHelper _logging;
        private readonly HttpContext _httpContext;

        public OrdersController(IOrderService context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _orderService = context;
            _configuration = configuration;
            _logging = new DAL.LoggingHelper(_configuration);
            _httpContext = httpContextAccessor.HttpContext;
        }

        
        [HttpPost]
        [Route("SaveOrder")]
        public async Task<ActionResult> SaveOrderByUser(OrderDTO order, int customerId)
        {


            try
            {
               DataTable orderItems = Helper.CreateDataTableFroOrderItems(order.OrderDetails.OrderItems);
                var responseDTO = await _orderService.SaveOrderByUser(order, orderItems, customerId);
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

        [HttpGet]
        [Route("OrderDetailsById")]
        public async Task<ActionResult> GetOrderDetails(int userId)
        {


            try
            {
                var responseDTO = await _orderService.GetAllOrdersById(userId);
                if (responseDTO.IsSuccess)
                {
                    return new OkObjectResult(new { MessageKey = "success", Result = new { Orders = responseDTO.OrderList } });
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
