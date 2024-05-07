using MobileAppAPI.BLL.DTO;
using MobileAppAPI.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppAPI.BLL.Providers
{
    public class OrderServiceProvider:IOrderService
    {

        private readonly IMobileAppDataRepository repository;
        public OrderServiceProvider(IMobileAppDataRepository repository)
        {
            this.repository = repository;
        }

        //public Task<OrderDTO> GetAllOrders()
        //{
        //    return repository.GetAllOrders();
        //}

        public Task<OrderDTO> SaveOrderByUser(OrderDTO order, DataTable dtOrders)
        {
            return repository.SaveOrderByUser(order, dtOrders);
        }

    }
}
