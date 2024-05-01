using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppAPI.BLL.DTO
{
    public class CategoryDTO
    {
        public CategoryDTO()
        {
            this.CategoryDetails = new List<CategoryInfo>();
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<CategoryInfo> CategoryDetails { get; set; }
    }
    public class CategoryInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
         public bool Active { get; set; }
        public string Image { get; set; }
    }
}
