using MobileAppAPI.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppAPI.BLL.Providers
{
    public class CategoryServiceProvider
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
