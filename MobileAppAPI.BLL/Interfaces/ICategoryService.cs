using MobileAppAPI.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppAPI.BLL.Interfaces
{
    public interface ICategoryService
    {

        Task<CategoryDTO> GetAllCategories();
       
    }
}
