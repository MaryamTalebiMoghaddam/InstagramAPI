
using AutoMapper;
using Azure.Core;
using GSF.Security.Cryptography;
using InstagramAPI.Data;
using InstagramAPI.Models;
using InstagramAPI.Models.DTO;
using InstagramAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Random = System.Random;

namespace InstagramAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private string secret;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public UserRepository(ApplicationDbContext db, UserManager<User> userManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _db = db;
            _userManager = userManager;
            secret = configuration.GetValue<string>("ApiSetting:Secret");
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<bool> CheckUserName(string username)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(t => t.UserName == username);
                if (user == null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred,this username already exists!", ex);
            }
        }

        public async Task<bool> ForgetPassword(ForgetPasswordDTO forgetPasswordDTO)
        {
            try
            {
                var exitingUser = await _db.Users.SingleOrDefaultAsync
                (u => u.PhoneNumber == forgetPasswordDTO.MobileNumber);

                if (exitingUser == null)
                {
                    return false;

                }
                exitingUser.TTL = DateTime.Now.AddMinutes(4);
                Random generator = new Random();
                exitingUser.ConfirmCode = generator.Next(10000, 99999);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred try again", ex);
            }

        }

        public async Task<string> Login(LoginRequestDTO loginDTO)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == loginDTO.Username);
                bool isValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

                if (isValid == true && user != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Name, user.UserName.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(3),
                        SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenResult = tokenHandler.CreateToken(tokenDescriptor);
                    LoginResponseDTO loginResponse = new LoginResponseDTO()
                    {
                        User= _mapper.Map<UserDTO>(user),
                        Token = tokenHandler.WriteToken(tokenResult),
                    };
                    return loginResponse.Token;
                }

                return null;
            }
            catch (Exception ex)
            {

                throw new Exception("Error occurred while login", ex);
            }
        }

        public async Task<bool> LogOut()
        {
            try
            {
                await _httpContextAccessor.HttpContext.SignOutAsync();
                _httpContextAccessor.HttpContext.Session.Clear();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<int> SignUp(SignupDTO signupDTO)
        {
            User user = new()
            {
                UserName = signupDTO.UserName,
                Name = signupDTO.Name,
                PhoneNumber = signupDTO.MobileNumber,
                TTL = DateTime.Now.AddMinutes(2),
                ConfirmCode = new Random().Next(10000, 99999)
            };
            try
            {
                var result = await _userManager.CreateAsync(user, signupDTO.Password);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.FirstOrDefault()?.Description);
                }
                return user.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while signing up", ex);
            }
        }



        public async Task<bool> ConfirmMobileNumber(int confirmCode, int id)
        {

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user != null && user.ConfirmCode == confirmCode && user.TTL >= DateTime.Now)
            {
                user.AuthorizedPhoneNumber = true;
                return true;
            }

            return false;

        }
    }
}
