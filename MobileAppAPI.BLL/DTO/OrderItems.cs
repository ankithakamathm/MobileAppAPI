namespace MobileAppAPI.BLL.DTO
{
    public class OrderItems
    {
        public string itemName { get; set; }
        public int itemQuantity { get; set; }

        public string currency { get; set; }

        public decimal price { get; set; }
        public decimal subTotal { get; set; }


    }
}