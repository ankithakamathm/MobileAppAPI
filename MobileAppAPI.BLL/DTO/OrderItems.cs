namespace MobileAppAPI.BLL.DTO
{
    public class OrderItems
    {
        public string itemName { get; set; }
        public int itemQuantity { get; set; }

        public string currency { get; set; }

        public double price { get; set; }
        public double subTotal { get; set; }


    }
}