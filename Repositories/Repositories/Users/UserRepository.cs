using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Business.Entity.User;
using Business.Interfaces.Repositories.User;

namespace Repositories.Repositories.Users
{
    public class UserRepository :DapperRepository, IUserRepository
    {
        #region Constructors
        public UserRepository(IConfiguration configuration)
                    : base(configuration)
        {
        }
        #endregion

        #region Public Methods
        public UserInfo GetUserByEmail(string email)
        {
            return QueryScalar<UserInfo>(
                        "dbo.User_GetUserByEmail",
                        new
                        {
                            Email = email
                        }
                );
        }

        public UserInfo GetUserById(int userId)
        {
            return QueryScalar<UserInfo>(
                        "dbo.User_GetByUserId",
                        new
                        { UserId = userId }
                    );
        }
        
        public List<UserInfo> GetUserList()
        {
            return QueryList<UserInfo>("dbo.User_GetUserList");
        }

        public void InsertUser(UserInfo user)
        {
            Execute(
                "dbo.User_Insert",
                new
                {
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.Address,
                    user.RoleId,
                    user.PhoneNumber,
                    user.PasswordHash,
                    user.PasswordSalt,
                    user.RoleId.Value,
                }
            );
        }

        #endregion
    }
}
