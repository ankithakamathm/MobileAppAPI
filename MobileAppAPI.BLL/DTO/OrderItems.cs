namespace MobileAppAPI.BLL.DTO
{
    public class OrderItems
    {
        public string itemName { get; set; }
        public int itemQuantity { get; set; }

        public string currency { get; set; }

        public float price { get; set; }
        public float subTotal { get; set; }


    }
}