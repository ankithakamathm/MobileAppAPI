﻿using MobileAppAPI.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileAppAPI.BLL.Interfaces
{
    public interface IProductService
    {

        Task<ProductDTO> GetAllProductsByCategory(int id);
       
    }
}
