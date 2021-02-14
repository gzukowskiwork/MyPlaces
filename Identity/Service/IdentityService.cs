using Identity.Response;
using Microsoft.AspNetCore.Identity;
using Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Requests;

namespace Identity.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationContext _applicationContext;

        public IdentityService(UserManager<ApplicationUser> userManager, ApplicationContext applicationContext)
        {
            _userManager = userManager;
            _applicationContext = applicationContext;
        }

        public async Task<RegistrationResponse> RegisterUserAsync(RegistrationRequest registrationRequest)
        {
            ApplicationUser applicationUser = await _userManager.FindByEmailAsync(registrationRequest.Email);
            if (applicationUser != null)
            {
                return new RegistrationResponse
                {
                    Success = false,
                    Errors = new[] { string.Format($"Users with email {registrationRequest.Email} exists", registrationRequest.Email) }
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
                    Errors = createdUser.Errors.Select(e=>e.Description)
                };
            }

            return new RegistrationResponse
            {
                Success = true,
                Token = ""
            };
        }

     
    }
}
