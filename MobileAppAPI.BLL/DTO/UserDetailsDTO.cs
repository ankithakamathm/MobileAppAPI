using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAppAPI.BLL
{
    public class UserDetailsDTO
    {
		public UserDetailsDTO()
        {
			this.UserDetails = new List<UserDetailsInfo>();
        }
		public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<UserDetailsInfo> UserDetails { get; set; }
    }
    public class UserDetailsInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        
        public bool IsAdmin { get; set; }
        public bool IsCustomer { get; set; }
        
    }
}
