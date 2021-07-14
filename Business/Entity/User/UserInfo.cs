using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Entity.User
{
    public class UserInfo
    {
		public int UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public int? RoleId { get; set; }
		public string PhoneNumber { get; set; }
		public string PasswordHash { get; set; }
		public string PasswordSalt { get; set; }
		public string Password { get; set; }
		public bool? IsDeleted { get; set; }
		public bool? NeedToApprove { get; set; }
		public DateTime? LastEditDate { get; set; }
		public DateTime? CreatedDate { get; set; }
	}
}
