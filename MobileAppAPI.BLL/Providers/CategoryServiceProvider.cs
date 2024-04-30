using MobileAppAPI.BLL.DTO;
using MobileAppAPI.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppAPI.BLL.Providers
{
    public class CategoryServiceProvider:ICategoryService
    {

        private readonly IMobileAppDataRepository repository;
        public CategoryServiceProvider(IMobileAppDataRepository repository)
        {
            this.repository = repository;
        }

        public Task<CategoryDTO> GetAllCategories()
        {
            return repository.GetAllCategories();
        }

    }
}
