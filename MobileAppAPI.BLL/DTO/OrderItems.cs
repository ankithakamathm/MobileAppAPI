using System;

namespace MobileAppAPI.BLL.DTO
{
    public class OrderItems
    {
        public int OrderItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemId { get; set; }
        
        public int ItemQuantity { get; set; }

        public string Currency { get; set; }
        public byte[] Image { get; set; }

        public decimal ItemPrice { get; set; }
        public decimal ItemTotal { get; set; }

      


    }
}