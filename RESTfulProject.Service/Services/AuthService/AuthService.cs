using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RESTfulProject.Repository.Entities;
using RESTfulProject.Repository.Repositories.UserRepository;
using RESTfulProject.Service.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RESTfulProject.Service.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AuthService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public UserDto GetUserByEmail(string email)
        {
            var user = _userRepository.GetByEmail(email);
            return _mapper.Map<UserDto>(user);
        }

        public int GetUserIdByEmail(string email)
        {
            return _userRepository.GetIdByEmail(email);
        }

        public UserDto GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            return _mapper.Map<UserDto>(user);
        }

        public UserDto UserRegistration(string email, string password, string passwordConfirm)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new UserDto
            {
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _userRepository.Add(_mapper.Map<User>(user));
            return user;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }

        public void SetRefreshTokenToUser(int userId, string token, DateTime created, DateTime expires)
        {
            var user = _userRepository.GetById(userId);
            user.RefreshToken = token;
            user.TokenCreated = created;
            user.TokenExpires = expires;
            _userRepository.Update(_mapper.Map<User>(user));
        }

        public string CreateToken(UserDto user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Email, user.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public bool UserExists(string email)
        {
            return _userRepository.Exists(email);
        }
    }
}
