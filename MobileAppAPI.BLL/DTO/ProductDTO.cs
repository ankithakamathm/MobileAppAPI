using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppAPI.BLL.DTO
{
    public class ProductDTO
    {
        public ProductDTO()
        {
            this.ProductDetails = new List<ProductDTOInfo>();
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ProductDTOInfo> ProductDetails { get; set; }
    }
    public class ProductDTOInfo
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public string Name { get; set; }
         public bool Available { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Attribute { get; set; }
        public string Currency { get; set; }
    }
}
