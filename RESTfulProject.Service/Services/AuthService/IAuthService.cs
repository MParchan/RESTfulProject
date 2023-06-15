using RESTfulProject.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Service.Services.AuthService
{
    public interface IAuthService
    {
        public UserDto GetUserByEmail(string email);
        public UserDto GetUserById(int id);
        public int GetUserIdByEmail(string email);
        public UserDto UserRegistration(string email, string password, string passwordConfirm);
        public void SetRefreshTokenToUser(int userId, string token, DateTime created, DateTime expires);
        public abstract bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        public string CreateToken(UserDto user);
        public bool UserExists(string email);
    }
}
