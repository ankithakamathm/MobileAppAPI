using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppAPI.BLL.DTO
{
    public class SubCategoryDTO
    {
        public SubCategoryDTO()
        {
            this.SubCategoryDetails = new List<SubCategoryInfo>();
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<SubCategoryInfo> SubCategoryDetails { get; set; }
    }
    public class SubCategoryInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
         public bool Active { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        
    }
}
