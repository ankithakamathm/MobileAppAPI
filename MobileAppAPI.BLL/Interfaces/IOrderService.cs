using MobileAppAPI.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppAPI.BLL.Interfaces
{
    public interface IOrderService
    {

        // Task<OrderDTO> GetAllOrders();

        Task<OrderDTO> SaveOrderByUser(OrderDTO order, DataTable dtOrders);


    }
}
