using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAppAPI.BLL
{
    public class UserRolesAndPermissionDTO
    {
		public UserRolesAndPermissionDTO()
        {
			this.UserCompanyRolePermissionInfo = new List<UserCompanyRolePermissionInfo>();
        }
		public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public UserCompanyRoleInfo UserCompanyRoleInfo { get; set; }
        public List<UserCompanyRolePermissionInfo> UserCompanyRolePermissionInfo { get; set; }
    }

    public class UserCompanyRoleInfo
    {
        public int UserSK { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public bool IsSuperAdmin { get; set; }
        public int DefaultCompanySK { get; set; }
        public int CompanyUserSK { get; set; }
        public int CompanyRoleSK { get; set; }
        public string RoleName { get; set; }
    }

    public class UserCompanyRolePermissionInfo
    {
        public int? CompanyRolePermissionSK { get; set; }
        public int PermissionSK { get; set; }
        public string PermissionName { get; set; }
        public string PermissionDescription { get; set; }
        public bool IsRead { get; set; }
        public bool IsWrite { get; set; }
        public bool IsDelete { get; set; }
    }

}
