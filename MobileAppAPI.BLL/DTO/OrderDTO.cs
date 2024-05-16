using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppAPI.BLL.DTO
{
    public class OrderDTO
    {
        public OrderDTO()
        {
            this.OrderDetails = new OrderInfo();
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public OrderInfo OrderDetails { get; set; }
    }
    public class OrderInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string zip_code { get; set; }
        public string Status { get; set; }
        public DateTime? OrderedDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public List<OrderItems> OrderItems { get; set; }
        public bool Active { get; set; }
        public decimal TotalPrice {  get; set; }
        public string Image { get; set; }
    }
}
