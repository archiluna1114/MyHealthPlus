using Business.Interfaces.Utilities;
using Business.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Business.Interfaces.Repositories.User;
using Business.Interfaces.Services;
using Business.Entity.User;
using Business.Utilities.Helper;
using Microsoft.Extensions.Options;
using Business.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Business.Entity;

namespace Business.Services.User
{
    public class UserService : IUserService
    {
        public UserService(IUserRepository userRepository,
            IHashingService hashService,
            IOptions<AppSettings> appSettings

            )
        {
            this._userRepository = userRepository;
            this._hashService = hashService;
            _appSettings = appSettings.Value;
        }

        private IUserRepository _userRepository;
        private IHashingService _hashService;
        private readonly AppSettings _appSettings;


        private string generateJwtToken(UserInfo user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private SaltedHash HashPassword(string password, string salt = "")
        {
            return _hashService.ComputeSaltedHash(password, salt);
        }

        private bool CheckPassword(string password, string dbPassword, string salt)
        {
            var hashedPass = HashPassword(password, salt);
            return hashedPass.Hash == dbPassword;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _userRepository.GetUserByEmail(model.Email);

             var isPasswordValid = CheckPassword(model.Password, user.PasswordHash, user.PasswordSalt);
            // return null if user not found
            if (!isPasswordValid) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public UserInfo GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public List<UserInfo> GetUserList()
        {
            return _userRepository.GetUserList();
        }

        public int InsertUser(UserInfo user)
        {
            var passwordHash = _hashService.ComputeSaltedHash(user.Password);

            user.PasswordHash = passwordHash.Hash; 
            user.PasswordSalt = passwordHash.Salt;

            _userRepository.InsertUser(user);

            return 0;
        }
    }
}
