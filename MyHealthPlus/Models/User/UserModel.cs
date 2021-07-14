using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHealthPlusWeb.Models.User
{
    public class UserModel
    {
		public int UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public int RoleId { get; set; }
		public int PhoneNumber { get; set; }
		public string Password { get; set; }
		public bool? IsDeleted { get; set; }
		public DateTime? LastEditDate { get; set; }
		public DateTime? CreatedDate { get; set; }
	}
}
