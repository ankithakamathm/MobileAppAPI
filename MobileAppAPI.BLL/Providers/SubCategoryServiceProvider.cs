using MobileAppAPI.BLL.DTO;
using MobileAppAPI.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppAPI.BLL.Providers
{
    public class SubCategoryServiceProvider:ISubCategoryService
    {

        private readonly IMobileAppDataRepository repository;
        public SubCategoryServiceProvider(IMobileAppDataRepository repository)
        {
            this.repository = repository;
        }

        public Task<SubCategoryDTO> GetAllSubCategories(int customerId)
        {
            return repository.GetAllSubCategories(customerId);
        }

    }
}
