
using MobileAppAPI.BLL.DTO;
using MobileAppAPI.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppAPI.BLL.Providers
{
    public class ThemeServiceProvider:IThemeService
    {

        private readonly IMobileAppDataRepository repository;
        public ThemeServiceProvider(IMobileAppDataRepository repository)
        {
            this.repository = repository;
        }
        
       
        public Task<ThemesDTO> GetThemeById(int id)
        {
            return repository.GetThemeById(id);
        }
       
    }
}
