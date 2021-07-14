using Business.Entity.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces.Repositories.User
{
    public interface IUserRepository
    {
        UserInfo GetUserByEmail(string email);
        UserInfo GetUserById(int userId);
        List<UserInfo> GetUserList();      
        void InsertUser(UserInfo user);
    }
}
