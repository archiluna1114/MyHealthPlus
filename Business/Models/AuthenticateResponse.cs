using Business.Entity.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? RoleId { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(UserInfo user, string token)
        {
            Id = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Token = token;
            RoleId = user.RoleId;
        }
    }

}
