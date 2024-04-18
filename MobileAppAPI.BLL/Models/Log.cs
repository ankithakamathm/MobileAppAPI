using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppAPI.BLL.Models
{
    public class Log
    {
        public string Description { get; set; }
        public string? Message { get; set; }
        public string? Exception { get; set; }
        public long? User { get; set; }
        //public DateTime CreatedDate { get; set; }
        public string? HostName { get; set; }

        public string? IPAddress { get; set; }


    }
}
