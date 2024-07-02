using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MobileAppAPI.BLL.DTO
{
    public class ThemesDTO
    {
        public ThemesDTO()
        {
            this.ThemesDetails = new ThemesInfo();
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ThemesInfo ThemesDetails { get; set; }
    }
    public class ThemesInfo
    {

        public int Id { get; set; }
        public int CustomerId { get; set; }

        public string Name { get; set; }
        public string ColorCode { get; set; }
       
        public string AppName { get; set; }        
        public byte[] Logo { get; set; }
        public string LogoImageName { get; set; }
        public string Description { get; set; }
        
        public string ButtonColor { get; set; }


        

    }
}

