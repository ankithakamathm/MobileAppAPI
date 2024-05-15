namespace MobileAppAPI.BLL.DTO
{
    public class OrderItems
    {
        public int OrderId { get; set; }
        public string itemName { get; set; }
        public int itemQuantity { get; set; }

        public string currency { get; set; }

        public decimal itemPrice { get; set; }
        public decimal itemTotal { get; set; }


    }
}