using Business.Entity.User;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        UserInfo GetUserById(int userId);
        List<UserInfo> GetUserList();
        int InsertUser(UserInfo user);
    }
}
