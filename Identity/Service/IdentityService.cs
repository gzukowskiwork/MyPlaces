using Entities.Model;
using Identity.Requests;
using Identity.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public IdentityService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }


        public async Task<RegistrationResponse> RegisterUserAsync(RegistrationRequest registrationRequest)
        {
            ApplicationUser applicationUser = await _userManager.FindByEmailAsync(registrationRequest.Email);
            if (applicationUser != null)
            {
                return new RegistrationResponse
                {
                    Success = false,
                    Errors = new[] { $"Users with email {registrationRequest.Email} already exists" }
                };
            }

            ApplicationUser newUser = new ApplicationUser
            {
                Email = registrationRequest.Email,
                UserName = registrationRequest.Email
            };

            IdentityResult createdUser = await _userManager.CreateAsync(newUser, registrationRequest.Password);

            if (!createdUser.Succeeded)
            {
                return new RegistrationResponse
                {
                    Errors = createdUser.Errors.Select(e => e.Description)
                };
            }

            return new RegistrationResponse
            {
                Success = true,
                Token = ""
            };
        }

        public async Task<RegistrationResponse> LoginAsync(LoginRequest loginRequest)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                return new RegistrationResponse
                {
                    Errors = new[] { $"User with email {loginRequest.Email} does not exist" },
                    Success = false
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

            if (!result)
            {
                return new RegistrationResponse
                {
                    Errors = new[] { "Invalid password" },
                    Success = false
                };
            }

            var claims = new[]
            {
                new Claim("Email", loginRequest.Email),
                new Claim("RegisteredUser", "true")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authenticate:key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Authenticate:Issuer"],
                audience: _configuration["Authenticate:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string writeToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new RegistrationResponse
            {
                Success = true,
                Token = writeToken
            };
        }



    }
}
