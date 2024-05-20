﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MobileAppAPI.BLL
{
    public class AddressDTO
    {
		public AddressDTO()
        {
			this.UserAddress = new Address();
        }
		public bool IsSuccess { get; set; }
        public string Message { get; set; }
        
        public Address UserAddress { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public string UserAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public int UserId { get; set; }
       
    }

  
}
