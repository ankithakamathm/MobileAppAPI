using MobileAppAPI.BLL.DTO;
using MobileAppAPI.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppAPI.BLL.Providers
{
    public class ProductServiceProvider: IProductService
    {

        private readonly IMobileAppDataRepository repository;
        public ProductServiceProvider(IMobileAppDataRepository repository)
        {
            this.repository = repository;
        }

        public Task<ProductDTO> GetAllProductsByCategory(int id)
        {
            return repository.GetAllProductsByCategory(id);
        }

        public Task<ProductDTO> GetAllProductsMatchingSearch(string product)
        {
            return repository.GetAllProductsMatchingSearch(product);

        }
        public Task<int> CheckIfProductAvailable(int id)
        {
            return repository.CheckIfProductAvailable(id);

        }

    }
}
