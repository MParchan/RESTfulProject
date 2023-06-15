using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTfulProject.API.ViewModels;
using RESTfulProject.Repository.Entities;
using RESTfulProject.Service.Services.AuthService;
using System.Linq;
using System.Security.Cryptography;

namespace RESTfulProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public ActionResult<User> Register(RegisterViewModel register)
        {
            var user = _authService.GetUserByEmail(register.Email);
            if (user != null)
            {
                return BadRequest("Email already in use.");
            }
            if (!register.Password.Equals(register.ConfirmPassword))
            {
                return BadRequest("Password and confirm password is not the same.");
            }
            return _mapper.Map<User>(_authService.UserRegistration(register.Email, register.Password, register.ConfirmPassword));
        }

        [HttpPost("Login")]
        public ActionResult<string> Login(LoginViewModel login)
        {
            var user = _authService.GetUserByEmail(login.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            if (!_authService.VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password");
            }
            string accessToken = _authService.CreateToken(user);
            var newrefreshToken = GenerateRefreshToken();
            _authService.SetRefreshTokenToUser(user.UserId, newrefreshToken.Token, newrefreshToken.Created, newrefreshToken.Expires);
            string refreshToken = newrefreshToken.Token;
            return Ok(new { accessToken, refreshToken });
        }

        [HttpPost("RefreshToken")]
        public ActionResult<string> RefreshToken(string email, string refreshToken)
        {
            if (!_authService.UserExists(email))
            {
                return BadRequest("User not exist");
            }
            var user = _authService.GetUserByEmail(email);
            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid refresh token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }
            string accessToken = _authService.CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            _authService.SetRefreshTokenToUser(user.UserId, newRefreshToken.Token, newRefreshToken.Created, newRefreshToken.Expires);
            refreshToken = newRefreshToken.Token;
            return Ok(new { accessToken, refreshToken});
        }

        private static RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(5),
                Created = DateTime.Now
            };
            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken, int userId)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires,
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
            _authService.SetRefreshTokenToUser(userId, newRefreshToken.Token, newRefreshToken.Created, newRefreshToken.Expires);
        }
    }
}
