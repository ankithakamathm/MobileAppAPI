using System;
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
            this.AddAddress = new Address();
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public Address AddAddress { get; set; }
        public List<Address> ListAddress { get; set; }
    }

    public class Address
    {
        public int Id { get; set; }
        public string UserAddress { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string Phone { get; set; }
        public int UserId { get; set; }
       
    }

  
}

